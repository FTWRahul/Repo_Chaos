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

    private PlayerController _player;

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
                if (!_player)
                {
                    _player = other.GetComponent<PlayerController>();
                }
                
                bool isRightItem = false;
                
                QuestGenerator questGenerator = other.GetComponent<QuestGenerator>();
                
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
                CheckForPlayerItems();
            }
        }
    }

    private IEnumerator StartWithDelay( SpawnedItem spawnedItem)
    {
        yield return new WaitForSeconds(.2f);
        spawnedItem.Vanish();
    }

    void CheckForPlayerItems()
    {
        QuestGenerator questGenerator = _player.GetComponent<QuestGenerator>();
        foreach (var item in questGenerator.itemsToCollect)
        {
            if (ItemsDatabase.Instance.itemsOnScene[item] < 1)
            {
                onQuestLost?.Invoke();
            }
        }
    }
}
