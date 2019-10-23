using UnityEngine;
using TMPro;

public class ExitCheck : MonoBehaviour
{
    public Events.EventWrongItem OnWrongItem;
    public Events.EventRightItem OnRightItem;
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
            if (other.GetComponent<PickUp>().itemHold != null)
            {
                bool isRightItem = false;
                ItemId item =  other.GetComponent<PickUp>().itemHold;
                for (int i = 0; i < other.GetComponent<QuestGenerator>().itemsToCollect.Count; i++)
                {
                    if (item.itemId == other.GetComponent<QuestGenerator>().itemsToCollect[i])
                    {
                        PaperListMenu.Instance.TextOrderedList[i].fontStyle = FontStyles.Strikethrough;
                        // Do the thing on UI
                        
                        // Play the right sound
                        isRightItem = true;
                        break;
                    }
                    else
                    {
                        isRightItem = false;
                    }
                }

                if (isRightItem)
                {
                    OnRightItem.Invoke();
                }
                else
                {
                    OnWrongItem.Invoke();
                }
                
                int itemID = item.itemId;
               ItemsDatabase.Instance.objectsInScene[itemID]--;
               other.GetComponent<PickUp>().DropItem();
               item.Vanish();
            }
        }
    }
}
