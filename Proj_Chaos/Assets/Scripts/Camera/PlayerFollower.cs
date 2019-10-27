using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private Transform characterCinemachine;
    private CinemachineFreeLook cinemachineFreeLook;
    public Transform target;
    private float vAngle;

    public void Start()
    {
        /*characterCinemachine = FindObjectOfType<InputSystem>().characterRoot;*/
        cinemachineFreeLook = FindObjectOfType<CinemachineFreeLook>();
        cinemachineFreeLook.Follow = characterCinemachine;
        cinemachineFreeLook.LookAt = characterCinemachine;
    }

/*    private void LateUpdate()
    {
        transform.position = target.position;
        vAngle -= Input.GetAxis("Mouse Y");
        vAngle = Mathf.Clamp(vAngle, 0, 30);
        transform.rotation = Quaternion.Euler(vAngle, target.eulerAngles.y, 0);
    }*/
}
