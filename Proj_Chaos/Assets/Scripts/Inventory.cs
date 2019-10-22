using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform root;
    private GameObject _itemToPickUp;
    private InputSystem _inputSystem;

    private void Start()
    {
        _inputSystem = GetComponent<InputSystem>();
        _inputSystem.OnEPressed.AddListener(PickUp);
    }

    private void PickUp()
    {
        _itemToPickUp.transform.SetParent(root);
        _itemToPickUp.GetComponent<ItemId>().isAvailable = false;
        _inputSystem.OnEPressed.RemoveListener(PickUp);
        _inputSystem.OnEPressed.AddListener(DropItem);
    }

    private void DropItem()
    {
        _itemToPickUp.transform.SetParent(null);
        _itemToPickUp.GetComponent<ItemId>().isAvailable = true;
        _inputSystem.OnEPressed.RemoveListener(DropItem);
        _inputSystem.OnEPressed.AddListener(PickUp);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // open pick up window
        ItemId item = other.GetComponent<ItemId>();

        if (item.isAvailable)
        {
            _itemToPickUp = item.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //close pick up window
        _itemToPickUp = null;
    }
}
