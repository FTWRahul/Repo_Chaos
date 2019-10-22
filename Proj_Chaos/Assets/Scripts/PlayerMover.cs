using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float mouseSensitivity;
    public float moveSpeed;
    private CharacterController _cc;

    private void Start()
    {
        _cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity, 0);
        
        Vector3 inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0,Input.GetAxisRaw("Vertical")).normalized;

        Vector3 moveDir = transform.rotation * inputDir + Physics.gravity;
        
        _cc.Move(moveSpeed * Time.deltaTime * moveDir);
    }
}
