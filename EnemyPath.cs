﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyPath : MonoBehaviour
{

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        
    }

    void GoToNextPoint()
    {
        if (points.Length == 0) return;
        {
            agent.destination = points[destPoint].position;
            destPoint = (destPoint + 1) % points.Length;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (points.Length == 0) return;
        
        agent.SetDestination(points[destPoint].transform.position);

        if(!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
    }
}
