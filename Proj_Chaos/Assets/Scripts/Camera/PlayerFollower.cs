﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public Transform target;
    private float vAngle;

    private void LateUpdate()
    {
        transform.position = target.position;
        vAngle -= Input.GetAxis("Mouse Y");
        vAngle = Mathf.Clamp(vAngle, 0, 30);
        transform.rotation = Quaternion.Euler(vAngle, target.eulerAngles.y, 0);
    }
}