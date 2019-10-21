using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        transform.position = target.TransformPoint(new Vector3(0, 2, -5));
        transform.rotation = Quaternion.Euler(20, target.eulerAngles.y, 0);
    }
}
