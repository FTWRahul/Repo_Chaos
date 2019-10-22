using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform root;
    private GameObject _itemToPickUp;

    public void OnPickUpPressed()
    {
        if (root.childCount == 0)
        {
            if (_itemToPickUp == null) return;
            
            _itemToPickUp.transform.SetParent(root);
            _itemToPickUp.transform.localPosition = Vector3.zero;
            _itemToPickUp.GetComponent<ItemId>().isAvailable = false;
        }
        else
        {
            root.GetChild(0).GetComponent<ItemId>().isAvailable = true;
            root.GetChild(0).transform.SetParent(null);
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        // open pick up window
        ItemId item = other.GetComponent<ItemId>();

        if (item != null && item.isAvailable)
        {
            _itemToPickUp = item.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //close pick up window
        if (other.gameObject == _itemToPickUp)
        {
            _itemToPickUp = null;
        }
    }
}
