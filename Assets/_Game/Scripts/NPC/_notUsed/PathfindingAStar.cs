using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathfindingAStar : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypoint;
    private NavMeshAgent agent;

    void Start()
    {
        currentWaypoint = 0;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[currentWaypoint].position);
    }

    void Update()
    {
        if (agent.remainingDistance <= 1)
        {
            currentWaypoint++;

            currentWaypoint = currentWaypoint > waypoints.Length ? 0 : currentWaypoint; 
            //if (currentWaypoint > waypoints.Length)
            //    currentWaypoint = 0;

            agent.SetDestination(waypoints[currentWaypoint].position);
                
        }
    }

}
