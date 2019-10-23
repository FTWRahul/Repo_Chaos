using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PickUp))]
public class Slapper : MonoBehaviour
{
    public Events.EventCharacterSlapped OnCharacterSlapped;
    public Events.EventCharacterAbleSlap OnCharacterEndSlap;
    
    [SerializeField] private float radius;
    [SerializeField] private float timeBetweenSlaps;
    [SerializeField] private float timeIncapacitated;
    [SerializeField] private float slapForce;
    
    [SerializeField] private bool canSlap = true;
    private Rigidbody _rb;
    private PickUp _pickUp;

    private void Start()
    {
        _pickUp = GetComponent<PickUp>();
        _rb = GetComponent<Rigidbody>();
    }

    public void OnSlapPressed()
    {
        if (canSlap)
        {
            Slap();
        }
    }

    private void Slap()
    {
        //should be on anim
        StartCoroutine(DidSlap());
        
        Collider[] overlappedObjects = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in overlappedObjects)
        {
            Slapper slapper = col.GetComponent<Slapper>();
            
            if (slapper != null && slapper != this)
            {
                StartCoroutine(slapper.Slapped(col.transform.position - transform.position));
                break;
            }
        }
    }

    private IEnumerator DidSlap()
    {
        ChangeBoolStates(false);
        
        yield return new WaitForSeconds(timeBetweenSlaps);

        ChangeBoolStates(true);
    }

    public void ChangeSlapBool(bool canSlap)
    {
        this.canSlap = canSlap;
    }
    
    private IEnumerator Slapped(Vector3 dir)
    {
        ChangeBoolStates(false);
        OnCharacterSlapped.Invoke();
        
        //Physics
        _rb.isKinematic = false;
        _rb.AddForce(dir.normalized * slapForce, ForceMode.Impulse);
        
        yield return new WaitForSeconds(timeIncapacitated);
        
        OnCharacterEndSlap.Invoke();
        
        //Physics
        if (_rb != null)
        {
            _rb.isKinematic = true;
        }
        
        ChangeBoolStates(true);
    }

    private void ChangeBoolStates(bool canDo)
    {
        ChangeSlapBool(canDo);
        _pickUp.ChangePickupBool(canDo);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
