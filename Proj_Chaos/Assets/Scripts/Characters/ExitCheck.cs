using System.Collections;
using UnityEngine;
using TMPro;

public class ExitCheck : MonoBehaviour
{
    public Events.EventWrongItem onWrongItem;
    public Events.EventRightItem onRightItem;
    public Events.EventItemRemoved onItemRemoved;
    
    private void OnTriggerEnter(Collider other)
    {
        PickupSystem pickupSystem = other.GetComponent<PickupSystem>();
        
        if (pickupSystem)
        {
            SpawnedItem spawnedItem = pickupSystem.GetItemHold();

            if (spawnedItem)
            {
                ItemsDatabase.Instance.itemsOnScene[spawnedItem.itemId]--;
                StartCoroutine(StartWithDelay(spawnedItem));
            }
            
            if (other.GetComponent<AgentController>())
            {
                Destroy(other.gameObject, 2f);
                NPCSpawner.spawnedAmount--;
            }
            else if(other.GetComponent<PlayerController>() && spawnedItem)
            {
                bool isRightItem = false;
                QuestGenerator questGenerator = other.GetComponent<QuestGenerator>();
                
                for (int i = 0; i < questGenerator.itemsToCollect.Count; i++)
                {
                    if (spawnedItem.itemId == questGenerator.itemsToCollect[i])
                    {
                        FindObjectOfType<QuestMenu>().StrikeItem(i);
                        questGenerator.itemsToCollect.RemoveAt(i);
                        isRightItem = true;
                        break;
                    }
                }
                
                if (isRightItem)
                {
                    onRightItem.Invoke();
                }
                else
                {
                    onWrongItem.Invoke();
                }
                
                onItemRemoved.Invoke();
            }
        }
    }

    private IEnumerator StartWithDelay( SpawnedItem spawnedItem)
    {
        yield return new WaitForSeconds(.2f);
        spawnedItem.Vanish();
    }
}
