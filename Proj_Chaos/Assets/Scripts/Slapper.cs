using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Slapper : MonoBehaviour
{
    public float radius;
    public float timeBetweenSlaps;
    public float timeIncapacitated;
    public float slapForce;
    public bool isPlayer;
    
    private bool _canSlap = true;

    private void Slap()
    {
        if (!_canSlap)
        {
            return;
        }
        
        ChangeSlap(false);
        
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

    public void ChangeSlap(bool shouldSlap)
    {
        _canSlap = shouldSlap;
    }
    
    public IEnumerator Slapped(Vector3 dir)
    {
        if (isPlayer)
        {
            GetComponent<PlayerMover>().enabled = false;
            GetComponent<CharacterController>().enabled = false;
        }
        else
        {
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<NavMeshAgentMover>().enabled = false;
        }

        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(dir.normalized * slapForce, ForceMode.Impulse);
        
        yield return new WaitForSeconds(timeIncapacitated);
        
        if (isPlayer)
        {
            GetComponent<PlayerMover>().enabled = true;
            GetComponent<CharacterController>().enabled = true;
        }
        else
        {
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<NavMeshAgentMover>().enabled = true;
        }
        
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
