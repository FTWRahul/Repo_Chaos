using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform root;
    private GameObject _itemToPickUp;
    private InputSystem _inputSystem;
    [SerializeField] public Collider collider;

    private void Start()
    {
        _inputSystem = GetComponent<InputSystem>();
        _inputSystem.OnEPressed.AddListener(PickUp);
        collider = GetComponentInChildren<Collider>();
    }

    public void PickUp()
    {
        if (_itemToPickUp != null)
        {
            _itemToPickUp.transform.SetParent(root);
            _itemToPickUp.transform.localPosition = Vector3.zero;
            _itemToPickUp.GetComponent<ItemId>().isAvailable = false;
            _inputSystem.OnEPressed.RemoveListener(PickUp);
            _inputSystem.OnEPressed.AddListener(DropItem);
        }
    }

    public void DropItem()
    {
        root.GetChild(0).GetComponent<ItemId>().isAvailable = true;
        root.GetChild(0).transform.SetParent(null);
        _inputSystem.OnEPressed.RemoveListener(DropItem);
        _inputSystem.OnEPressed.AddListener(PickUp);
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
        _itemToPickUp = null;
    }
}
