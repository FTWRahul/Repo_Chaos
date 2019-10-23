using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PickUp))]
public class Slapper : MonoBehaviour
{
    public Events.EventCharacterSlap OnCharacterSlap;
    public Events.EventCharacterSlapped OnCharacterSlapped;
    public Events.EventCharacterAbleSlap OnCharacterEndSlap;
    
    [SerializeField] private float radius;
    [SerializeField] private float timeBetweenSlaps;
    [SerializeField] private float timeIncapacitated;
    [SerializeField] private float slapForce;
    [SerializeField] private float probabilityOfAbleToSlap;
    
    [SerializeField] private bool canSlap;
    private Rigidbody _rb;
    private PickUp _pickUp;

    private void Start()
    {
        _pickUp = GetComponent<PickUp>();
        _rb = GetComponent<Rigidbody>();
        
        if (Random.value < probabilityOfAbleToSlap)
        {
            canSlap = true;
        }

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
        OnCharacterSlap.Invoke();
        
        //Coroutine doesnt work
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
        ChangeBoolStates();
        
        yield return new WaitForSeconds(timeBetweenSlaps);

        ChangeBoolStates();
    }

    
    private IEnumerator Slapped(Vector3 dir)
    {
        OnCharacterSlapped.Invoke();
        ChangeBoolStates();
        
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
        
        ChangeBoolStates();
    }

    private void ChangeBoolStates()
    {
        ChangeSlapBool(!canSlap);
        _pickUp.ChangePickupBool(canSlap);
    }

    public void ChangeSlapBool(bool canDoAction)
    {
        canSlap = canDoAction;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
