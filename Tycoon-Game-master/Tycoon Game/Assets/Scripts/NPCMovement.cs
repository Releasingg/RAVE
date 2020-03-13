using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour {

    public Transform Exit;
    public GameManager gameManager;

    int VisitedObjects = 0;
    int ObjectsToVisit = 0;
    float DistanceToObject = 0f;
    float MinimalDistance = 2f;
    float Timer = 0f;
    float WaitTime = 2f;
    bool ReachedWaypoint = false;

    List<Transform> Waypoints = new List<Transform>();
    NavMeshAgent navMeshAgent;

    // Use this for initialization
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        if (Exit == null)
        {
            Exit = GameObject.FindGameObjectWithTag("Exit").GetComponent<Transform>();
        }

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Display"))
        {
            Waypoints.Add(g.transform);
            ObjectsToVisit++;
        }

        Utilitys.ShuffleArray<Transform>(Waypoints);
    }

    // Update is called once per frame
    void Update()
    {
        if (VisitedObjects < ObjectsToVisit)
        {
            DistanceToObject = Vector3.Distance(Waypoints[VisitedObjects].position, transform.position);
            WaitTime = Waypoints[VisitedObjects].gameObject.GetComponent<Display>().ReturnWaitingAmount();
        }

        if (ObjectsToVisit > VisitedObjects && DistanceToObject > MinimalDistance && !ReachedWaypoint)
        {            
            MoveToWaypoint(Waypoints[VisitedObjects]);
        }

       // if (DistanceToObject < MinimalDistance && Timer <= WaitTime)
       // {
       //     ReachedWaypoint = true;
       //     Timer += Time.deltaTime;
       // }

       // if (Timer >= WaitTime && ObjectsToVisit > VisitedObjects)
        //{
       //     VisitedObjects++;
        //    ReachedWaypoint = false;
       //     Timer = 10;
       // }

        //if (VisitedObjects == ObjectsToVisit && !ReachedWaypoint)
        //{
        //    MoveToWaypoint(Exit);
        //}

        if (Vector3.Distance(Exit.position, transform.position) <= MinimalDistance && VisitedObjects == ObjectsToVisit)
        {
            gameManager.RemoveVisitor(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered...");
        if (other.CompareTag("Museum"))
        {
            Debug.Log("...Museum");
            gameManager.VisitorCountInHouse++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited...");
        if (other.CompareTag("Museum"))
        {
            Debug.Log("...Museum");
            gameManager.VisitorCountInHouse--;
        }
    }

    void MoveToWaypoint(Transform way)
    {
        navMeshAgent.destination = way.position;
    }

}
