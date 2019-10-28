using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PickupSystem : MonoBehaviour
{
    [HideInInspector] public Events.EventItemPickup onItemPickup;
    [HideInInspector] public Events.EventItemDrop onItemDrop;
    [HideInInspector] public Events.EventPickupVariableChange onPickupVariableChange;
    
    [FormerlySerializedAs("root")] [SerializeField] private Transform itemHolder;
    
    private bool _canPickup = true;
    private SpawnedItem _itemHold;
    private GameObject _desireItem;

    public void OnPickUpCalled()
    {
        if (_canPickup && _itemHold == null && _desireItem!= null)
        {
            if (_canPickup)
            {
                PickUpItem();
            }
        }
        else if (_itemHold != null)
        {
            DropItem();
        }
    }

    private void PickUpItem()
    {
        onItemPickup.Invoke();

        ChangePickupBool(false);

        _itemHold = _desireItem.GetComponent<SpawnedItem>();
        _itemHold.transform.SetParent(itemHolder);
        _itemHold.transform.localPosition = Vector3.zero;
        _itemHold.transform.rotation = Quaternion.identity;
        _itemHold.GetComponent<Rigidbody>().isKinematic = true;
        _itemHold.isAvailable = false;
    }

    public void DropItem()
    {
        onItemDrop.Invoke();

        if (_itemHold != null)
        {
            _itemHold.transform.SetParent(null); 
            _itemHold.GetComponent<Rigidbody>().isKinematic = false; 
            _itemHold.isAvailable = true;
        }

        _itemHold = null; 
        _desireItem = null;
        
        ChangePickupBool(true);
    }

    public void SelectItem(GameObject item)
    {
        _desireItem = item;
    }

    public SpawnedItem GetItemHold()
    {
        return _itemHold;
    }

    private void ChangePickupBool(bool action)
    {
        _canPickup = action;
        onPickupVariableChange.Invoke(action);
    }
}
