using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PointNClick : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    private NavMeshAgent agent;

    public GameObject slope;
    public NavMeshSurface surface;
    bool slopeActive;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        slopeActive = slope.activeInHierarchy;
    }

    IEnumerator waitOneFrame()
    {
        yield return null;
        surface.BuildNavMesh();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            slopeActive = !slopeActive;
            slope.SetActive(slopeActive);
            StartCoroutine(waitOneFrame());
        }


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                target.position = hit.point + new Vector3(0, 0.2F, 0);
                agent.SetDestination(target.position);
            }
        }
    }
}
