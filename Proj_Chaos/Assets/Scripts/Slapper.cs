using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Slapper : MonoBehaviour
{
    public Events.EventCharacterSlapped OnCharacterSlapped;
    public Events.EventCharacterAbleSlap OnCharacterAbleSlap;
    
    public float radius;
    public float timeBetweenSlaps;
    public float timeIncapacitated;
    public float slapForce;

    private Rigidbody _rb;
    private bool _canSlap = true;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Slap()
    {
        if (!_canSlap) return;

        StartCoroutine(DidSlap());
        
        Collider[] overlappedObjects = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in overlappedObjects)
        {
            Slapper slapper = col.GetComponent<Slapper>();
            
            if (slapper != null && slapper != this)
            {
                StartCoroutine(slapper.Slapped(col.transform.position - transform.position));
            }
        }
    }

    private IEnumerator DidSlap()
    {
        ChangeSlap(false);
        
        yield return new WaitForSeconds(timeBetweenSlaps);
        
        ChangeSlap(true);
    }

    public void ChangeSlap(bool shouldSlap)
    {
        _canSlap = shouldSlap;
    }

    private IEnumerator Slapped(Vector3 dir)
    {
        ChangeSlap(false);
        
        OnCharacterSlapped.Invoke();

        _rb.isKinematic = false;
        _rb.AddForce(dir.normalized * slapForce, ForceMode.Impulse);
        
        yield return new WaitForSeconds(timeIncapacitated);
        
        OnCharacterAbleSlap.Invoke();

        _rb.isKinematic = true;
        
        ChangeSlap(true);
    }
}
