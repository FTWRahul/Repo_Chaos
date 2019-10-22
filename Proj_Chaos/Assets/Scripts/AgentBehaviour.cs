using System;
using UnityEngine;
using UnityEngine.AI;

public class AgentBehaviour : MonoBehaviour
{
    public Events.EventEPressed OnEPressed;
    public Events.EventLMBPressed OnLMBPressed;

    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    

    public void SetDestination(Transform pos)
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
               
                Invoke("Invokerboop", 0.1f);
            }
        }
        else if( other.GetComponent<Slapper>())
        {
            OnLMBPressed.Invoke();
        }
        
    }

    public void Invokerboop()
    {
        OnEPressed.Invoke();

    }
}
