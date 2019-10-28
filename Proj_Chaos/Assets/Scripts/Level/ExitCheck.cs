using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class ExitCheck : MonoBehaviour
{
    [HideInInspector] public Events.EventQuestLost onQuestLost;
    public Events.EventWrongItem onWrongItem;
    public Events.EventRightItem onRightItem;
    public Events.EventItemRemoved onItemRemoved;

    private void OnTriggerEnter(Collider other)
    {
        PickupSystem pickupSystem = other.GetComponent<PickupSystem>();
        if (pickupSystem)
        {
            if (other.GetComponent<AgentController>())
            {
                SpawnedItem spawnedItem = pickupSystem.GetItemHold();
                if (spawnedItem)
                {
                    ItemsDatabase.Instance.itemsOnScene[spawnedItem.itemId]--;
                    StartCoroutine(StartWithDelay(spawnedItem));
                }
                
                Destroy(other.gameObject, 1.5f);
                NPCSpawner.spawnedAmount--;
            }
            else if(other.GetComponent<PlayerController>())
            {
                PlayerController _player = other.GetComponent<PlayerController>();
                QuestGenerator questGenerator = other.GetComponent<QuestGenerator>();
                SpawnedItem spawnedItem = pickupSystem.GetItemHold();

                if (spawnedItem)
                {
                    bool isRightItem = false;

                    for (int i = 0; i < questGenerator.itemsToCollect.Count; i++)
                    {
                        if (spawnedItem.itemId == questGenerator.itemsToCollect[i])
                        {
                            FindObjectOfType<QuestMenu>().StrikeItem(spawnedItem.itemId);
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
                    ItemsDatabase.Instance.itemsOnScene[spawnedItem.itemId]--;
                    StartCoroutine(StartWithDelay(spawnedItem));
                }
                
                CheckForPlayerItems(questGenerator);
            }
        }
    }

    private IEnumerator StartWithDelay( SpawnedItem spawnedItem)
    {
        yield return new WaitForSeconds(.2f);
        spawnedItem.Vanish();
    }

    private void CheckForPlayerItems(QuestGenerator questGenerator)
    {
        foreach (var item in questGenerator.itemsToCollect)
        {
            if (ItemsDatabase.Instance.itemsOnScene[item] < 1)
            {
                onQuestLost?.Invoke();
            }
        }
    }
}
