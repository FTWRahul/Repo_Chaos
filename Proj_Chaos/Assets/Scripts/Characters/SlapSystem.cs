using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlapSystem : MonoBehaviour
{
    [HideInInspector] public Events.EventCharacterSlap onCharacterSlap;
    [HideInInspector] public Events.EventCharacterSlapped onCharacterSlapped;
    [HideInInspector] public Events.EventCharacterEndSlap onCharacterEndSlap;

    [SerializeField] private bool canSlap;
    [SerializeField] private float radius;
    [SerializeField] private float timeBetweenSlaps;
    [SerializeField] private float timeIncapacitated;
    [SerializeField] private float slapForce;

    [SerializeField] private Transform rayOrigin;
    [SerializeField] private float rayDistance;
    
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnSlapCalled()
    {
        if (canSlap)
        {
            Slap();
        }
    }

    private void Slap()
    {
        onCharacterSlap.Invoke();
        StartCoroutine(SlapCoroutine());
        
        Collider[] overlappedObjects = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in overlappedObjects)
        {
            SlapSystem slapSystem = col.GetComponent<SlapSystem>();
            
            if (slapSystem != null && slapSystem != this)
            {
                StartCoroutine(slapSystem.Slapped(col.transform.position - transform.position));
                break;
            }
        }
    }

    private IEnumerator SlapCoroutine()
    {
        ChangeSlapBool(false);
        yield return new WaitForSeconds(timeBetweenSlaps);
        ChangeSlapBool(true);
    }

    public void ChangeSlapBool(bool action)
    {
        canSlap = action;
    }

    private IEnumerator Slapped(Vector3 dir)
    {
        Debug.Log(gameObject.name + " was slapped");
        onCharacterSlapped.Invoke();
        
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(dir.normalized * slapForce, ForceMode.Impulse);
        
        yield return new WaitForSeconds(timeIncapacitated);
        
        _rigidbody.isKinematic = true;
        onCharacterEndSlap.Invoke();
        ChangeSlapBool(true);
    }
}
