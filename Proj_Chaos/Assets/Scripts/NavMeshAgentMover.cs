using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentMover : MonoBehaviour
{

    private void OnEnable()
    {
        GetComponent<NavMeshAgent>().SetDestination(transform.position - new Vector3(25, 0, 0));
    }
}
