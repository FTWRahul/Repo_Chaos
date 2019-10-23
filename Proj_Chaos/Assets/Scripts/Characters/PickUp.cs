using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Slapper))]
public class PickUp : MonoBehaviour
{
    public bool canDoAction = true;
    public bool hasItem = false;

    public Events.EventCharacterPickup onCharacterPickup;
    public Events.EventCharacterHasItemChange onHasItemChange;
    // event on OnpickupItem
    // event onHasItemChange(bool)
    // event on onDropItem

    
    [SerializeField] private Transform root;
    private GameObject _itemToPickUp;
    [FormerlySerializedAs("_itemHold")] public ItemId itemHold;
    private Slapper _slapper;

    private void Start()
    {
        _slapper = GetComponent<Slapper>();
        _slapper.OnCharacterSlapped.AddListener(DropItem);
    }

    public void OnPickUpPressed()
    {
        if (canDoAction)
        {
            if (!hasItem)
            {
                if (_itemToPickUp == null) return;
                
                PickUpItem();
            }
            else if (hasItem)
            {
                DropItem();
            }
        }
    }

    private void PickUpItem()
    {
        onCharacterPickup.Invoke();
        
        ChangePickupBool(false);
        _slapper.ChangeSlapBool(false);
        //trigger pick up anim
        hasItem = true;

        itemHold = _itemToPickUp.GetComponent<ItemId>();
        itemHold.transform.SetParent(root);
        itemHold.transform.localPosition = Vector3.zero;
        itemHold.transform.rotation = Quaternion.identity;
        itemHold.GetComponent<Rigidbody>().isKinematic = true;
        
        itemHold.isAvailable = false;
        
        //Should call this method from anim
        ChangePickupBool(true);
    }

    private void DropItem()
    {
        //additional check for dropping item after slap//
        if (!hasItem) return;
        
        ChangePickupBool(false);
        //trigger pick up anim
        hasItem = false;
        
        itemHold.GetComponent<Rigidbody>().isKinematic = false;
        itemHold.isAvailable = true;
        itemHold.transform.SetParent(null);
        itemHold = null;
        
        //Should call this method from anim
        ChangePickupBool(true);
        _slapper.ChangeSlapBool(true);
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

    public void ChangePickupBool(bool canDo)
    {
        onHasItemChange.Invoke();
        canDoAction = canDo;
    }
}
