using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public Events.EventEPressed OnEPressed;
    public Events.EventLMBPressed OnLMBPressed;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnEPressed.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnLMBPressed.Invoke();
        }
    }
}
