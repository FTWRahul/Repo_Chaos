using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MonoBehaviour
{
    [HideInInspector] public Events.EventUpdateMovement onUpdateMovement;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float t = 1;
    
    private Camera _camera;
    private CharacterController _characterController;
    
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0), t);
         /*= new Vector3( 0,  _camera.transform.eulerAngles.y, 0);*/
        /*transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity, 0);*/

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Vector3 inputDir = new Vector3(horizontal, 0,vertical).normalized;
        Vector3 moveDir = transform.rotation * inputDir + Physics.gravity;
        
        _characterController.Move(moveSpeed * Time.deltaTime * moveDir);
        
        onUpdateMovement.Invoke(horizontal,vertical);
    }
}
