using UnityEngine;

[RequireComponent(typeof(Slapper))]
public class PickUp : MonoBehaviour
{
    public bool canDoAction = true;
    public bool hasItem = false;
    
    [SerializeField] private Transform root;
    private GameObject _itemToPickUp;
    private ItemId _itemHold;
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

    public void PickUpItem()
    {
        ChangePickupBool(false);
        _slapper.ChangeSlapBool(false);
        //trigger pick up anim
        hasItem = true;

        _itemHold = _itemToPickUp.GetComponent<ItemId>();
        _itemHold.transform.SetParent(root);
        _itemHold.transform.localPosition = Vector3.zero;
        _itemHold.GetComponent<Rigidbody>().isKinematic = true;
        
        _itemHold.isAvailable = false;
        
        //Should call this method from anim
        ChangePickupBool(true);
    }

    public void DropItem()
    {
        //additional check for dropping item after slap//
        if (!hasItem) return;
        
        ChangePickupBool(false);
        //trigger pick up anim
        hasItem = false;
        
        _itemHold.GetComponent<Rigidbody>().isKinematic = false;
        _itemHold.isAvailable = true;
        _itemHold.transform.SetParent(null);
        _itemHold = null;
        
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
        canDoAction = canDo;
    }
}
