using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;

public class FSMSimple : MonoBehaviour
{

    #region FSM States
    public enum FSMStates { Waypoints, Chasing, Shooting, Firing, Die, Stunned };
    public FSMStates state = FSMStates.Waypoints;
    #endregion

    #region Generic Variable
    public GameObject target;
    public float rotSpeed = 20;
    private float timer;
    private Vector3 dir;
    private NavMeshAgent agent;
    #endregion

    #region Wapypoints
    public Transform[] waypoints;
    public float distanceToChangeWaypoint;
    private int currentWaypoint;
    #endregion

    #region Chasing
    public float distanceToStartChasing;
    public float distanceToStopChasing;
    public float distanceToAttack;
    public float distanceToReturnChase;
    public float chanceToFire;
    #endregion

    #region Shooting
    public Rigidbody bullet;
    public Transform muzzle;
    public float bulletInitialForce;
    public float frequency;
    public int maxNumberOfShoots;
    private int numberOfShoots;
    #endregion

    public Image playerHealthPoints;

    #region Unity Functions
    public void Start()
    {
        currentWaypoint = 0;
        timer = 0;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[currentWaypoint].position);
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (playerHealthPoints.fillAmount <= 0)
        {
            agent.SetDestination(waypoints[currentWaypoint].position);
            state = FSMStates.Waypoints;
        }
    }

    public void FixedUpdate()
    {
        dir = target.transform.position - transform.position;

        switch (state)
        {
            case FSMStates.Waypoints: WaypointState(); break;
            case FSMStates.Chasing: ChaseState(); break;
            case FSMStates.Shooting: ShootState(); break;
            case FSMStates.Stunned: Stunned(); break;

            default: print("BUG: state should never be on default clause"); break;
        }
    }
    #endregion

    #region Waypoints State
    private void WaypointState()
    {
        if (state != FSMStates.Stunned)
        {
            agent.isStopped = false;
            agent.SetDestination(waypoints[currentWaypoint].position);
            state = FSMStates.Waypoints;
            // Check if target is in range to chase
            if (dir.magnitude <= distanceToStartChasing && state != FSMStates.Stunned)
            {
                state = FSMStates.Chasing;
                return;
            }

            // Find the direction to the current waypoint,
            //   rotate and move towards it

            if (agent.remainingDistance <= distanceToChangeWaypoint && state != FSMStates.Stunned)
            {
                agent.isStopped = false;
                currentWaypoint++;
                if (currentWaypoint >= waypoints.Length)
                    currentWaypoint = 0;
                agent.SetDestination(waypoints[currentWaypoint].position);

            }
        }
    }
    #endregion

    #region Chasing State
    private void ChaseState()
    {
        if (state != FSMStates.Stunned)
        {
            // Check if target is close enough to shoot
            //   or if target is too far way, then return to Waypoints
            if (dir.magnitude > distanceToStopChasing)
            {
                state = FSMStates.Waypoints;
                agent.SetDestination(waypoints[currentWaypoint].position);
                return;
            }
            else if (dir.magnitude <= distanceToAttack)
            {
                timer = 0;
                agent.isStopped = true;
                state = FSMStates.Shooting;
                numberOfShoots = 0;
                return;
            }
            agent.SetDestination(target.transform.position);
        }
    }
    #endregion

    #region Shooting State
    private void ShootState()
    {
        if (state != FSMStates.Stunned)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotSpeed);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            timer += Time.deltaTime;
            if (timer >= frequency)
            {
                timer = 0;

                Rigidbody b = GameObject.Instantiate(bullet, muzzle.position, muzzle.rotation) as Rigidbody;
                b.AddForce(muzzle.forward * bulletInitialForce);
                //GameObject.FindWithTag("soundcontrol").GetComponent<SoundControl>().PlaySound("shoot");

                numberOfShoots++;
                if (numberOfShoots >= maxNumberOfShoots)
                {
                    if (dir.magnitude < distanceToAttack)
                        numberOfShoots = 0;
                    else if (dir.magnitude > distanceToAttack && dir.magnitude <= distanceToReturnChase)
                    {
                        state = FSMStates.Chasing;
                        agent.isStopped = false;
                        agent.SetDestination(target.transform.position);
                    }
                    else if (dir.magnitude > distanceToReturnChase)
                    {
                        state = FSMStates.Waypoints;
                        agent.isStopped = false;
                        agent.SetDestination(waypoints[currentWaypoint].position);
                    }
                }
            }
        }
    }
    #endregion



    private void Stunned()
    {
        StartCoroutine(WaitForStun());
    }

    IEnumerator WaitForStun()
    {
        yield return new WaitForSeconds(5);
        state = FSMStates.Waypoints;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
            state = FSMStates.Stunned;
    }
}