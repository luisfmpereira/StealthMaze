using UnityEngine;
public class PathFindingWayPoints : MonoBehaviour 
{
	public  float       speed    = 5;
	public  float       rotSpeed = 10;
	public  Transform[] waypoints;
	private int         currentWayPoint;
	private Rigidbody   rb;

	public void Start() 
    {
		currentWayPoint = 0;
		rb              = GetComponent<Rigidbody>();
	}

	public void FixedUpdate() 
    {
		Vector3 dir           = waypoints[currentWayPoint].position - transform.position;
		transform.rotation    = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotSpeed);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

		if (dir.sqrMagnitude <= 1) 
        {
			currentWayPoint++;
			if (currentWayPoint >= waypoints.Length)
				currentWayPoint = 0;
		} 
        else
			rb.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
	}
}