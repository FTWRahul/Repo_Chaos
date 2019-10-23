using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AgentBehaviour>() != null)
        {
            if (other.GetComponent<PickUp>().itemHold != null)
            {
                int itemID = other.GetComponent<PickUp>().itemHold.itemId;
                ItemsDatabase.Instance.objectsInScene[itemID]--; 
            }
            Destroy(other.gameObject, 2f);
        }
        else if (other.GetComponent<PlayerMover>())
        {
            
        }
    }
}
