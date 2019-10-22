using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Slapper : MonoBehaviour
{
    public float radius;
    public float timeBetweenSlaps;
    public float timeIncapacitated;
    private Collider[] _overlappedObjects;

    private void Slap()
    {
        Physics.OverlapSphereNonAlloc(transform.position, radius, _overlappedObjects);

        foreach (Collider col in _overlappedObjects)
        {
            Slapper slapper = col.GetComponent<Slapper>();

            if (slapper != null)
            { 
                slapper.Slapped(); 
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Slap();
        }
    }

    public void Slapped()
    {
        
    }
}
