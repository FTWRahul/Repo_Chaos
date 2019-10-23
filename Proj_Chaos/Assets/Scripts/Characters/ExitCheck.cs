using System.Collections;
using UnityEngine;
using TMPro;

public class ExitCheck : MonoBehaviour
{
    public Events.EventWrongItem OnWrongItem;
    public Events.EventRightItem OnRightItem;
    public Events.EventItemRemoved OnItemRemoved;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AgentBehaviour>() != null)
        {
            if (other.GetComponent<PickUp>().itemHold != null)
            {
                int itemID = other.GetComponent<PickUp>().itemHold.itemId;
                ItemsDatabase.Instance.objectsInScene[itemID]--; 
                OnItemRemoved.Invoke();
            }
            Destroy(other.gameObject, 5f);
        }
        else if (other.GetComponent<CharacterController>() != null)
        {
            Debug.Log("WHAT WHAT");
            bool isRightItem = false;
            if (other.GetComponent<PickUp>().itemHold != null)
            {            
                Debug.Log("IN THE BUTT");

                ItemId item =  other.GetComponent<PickUp>().itemHold;
                for (int i = 0; i < other.GetComponent<QuestGenerator>().itemsToCollect.Count; i++)
                {
                    if (item.itemId == other.GetComponent<QuestGenerator>().itemsToCollect[i])
                    {
                        TextMeshProUGUI text = PaperListMenu.Instance.TextOrderedList[i];
                        int idtoRemove = other.GetComponent<QuestGenerator>().itemsToCollect[i];
                        PaperListMenu.Instance.TextOrderedList.Remove(text);
                        other.GetComponent<QuestGenerator>().itemsToCollect.RemoveAt(i);
                        text.fontStyle = FontStyles.Strikethrough;
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
               OnItemRemoved.Invoke();
               StartCoroutine(StartWithDelay(other, item));
            }
        }
        
    }

    public IEnumerator StartWithDelay(Collider other, ItemId item)
    {
        yield return new WaitForSeconds(.2f);
        item.Vanish();
    }

}
