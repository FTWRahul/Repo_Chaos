using System;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentMover : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    
    public Events.EventEPressed OnEPressed;
    public Events.EventLMBPressed OnLMBPressed;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        _navMeshAgent.SetDestination(transform.position - new Vector3(25, 0, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemId item = other.GetComponent<ItemId>();

        if (item != null) //also check if this item is item that i'm looking for
        {
            if (item.isAvailable)
            {
                OnEPressed.Invoke();
            }
            else
            {
                OnLMBPressed.Invoke();
            }
        }
    }
}
