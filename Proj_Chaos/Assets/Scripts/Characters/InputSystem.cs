using UnityEngine;

public class InputSystem : MonoBehaviour
{
    [HideInInspector] public Events.EventItemSelection onItemSelection;
    [HideInInspector] public Events.EventPickupCall onPickupCall;
    [HideInInspector] public Events.EventSlapCall onSlapCall;
    
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform point;

    private Camera _camera;
    private SpawnedItem _selection;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.RUNNING)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                onPickupCall.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                onSlapCall.Invoke();
            }

            if (_selection != null)
            {
                _selection.Dehighlight();
                _selection = null;
            }
        
            Ray ray = _camera.ScreenPointToRay(point.position);

            if (Physics.Raycast(ray, out RaycastHit hit, 7, layerMask))
            {
                SpawnedItem selection = hit.transform.GetComponent<SpawnedItem>();
                if (selection.isAvailable)
                {
                    selection.Highlight();
                    _selection = selection;

                    onItemSelection?.Invoke(selection.gameObject);
                }
            }
        }
        
    }
}
