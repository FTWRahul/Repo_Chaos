﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void OnCollisionStay(Collision other)
    {
        if (other.collider.CompareTag("Shelf"))
        {
            Debug.Log("BOOP");
            Destroy(gameObject);
        }
    }
}
