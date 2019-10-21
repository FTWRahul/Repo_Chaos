using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float mouseSensitivity;
    public float moveSpeed;
    public CharacterController cc;
    
    private void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity, 0);
        
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0,Input.GetAxisRaw("Vertical")).normalized;
        
        cc.Move((transform.rotation * dir) * moveSpeed * Time.deltaTime);
    }
}
