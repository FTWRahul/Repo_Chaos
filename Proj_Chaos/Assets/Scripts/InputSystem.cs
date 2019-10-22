using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public Events.EventEPressed OnEPressed;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnEPressed.Invoke();
        }
    }
}
