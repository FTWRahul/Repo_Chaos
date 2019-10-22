using System;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentMover : MonoBehaviour
{
    public NavMeshAgent _navMeshAgent;
    
    public Events.EventEPressed OnEPressed;
    public Events.EventLMBPressed OnLMBPressed;

    private void OnEnable()
    {
        _navMeshAgent.SetDestination(transform.position - new Vector3(25, 0, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ItemId>())
        {
            ItemId item = other.GetComponent<ItemId>();

            if (item.isAvailable) //also check if this item is item that i'm looking for
            {
                OnEPressed.Invoke();
            }
        }
        else if( other.GetComponent<Slapper>())
        {
            OnLMBPressed.Invoke();
        }
        
    }
}
