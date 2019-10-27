using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class CharacterAnimation : MonoBehaviour
{
    private Animator _animator;

    private static readonly int HorizontalF = Animator.StringToHash("Horizontal_f");
    private static readonly int VerticalF = Animator.StringToHash("Vertical_f");
    private static readonly int Index = Animator.StringToHash("Index");
    private static readonly int PickUp = Animator.StringToHash("PickUp");
    private static readonly int Slap = Animator.StringToHash("Slap");
    private static readonly int Slapped = Animator.StringToHash("Slapped");
    private static readonly int HasItem = Animator.StringToHash("HasItem");
    
    public void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void UpdateMovement(float horizontal, float vertical)
    {
        _animator.SetFloat(HorizontalF, horizontal);
        _animator.SetFloat(VerticalF, vertical);
        
        /*if (_characterController)
        {
            _animator.SetFloat(HorizontalF, Input.GetAxisRaw("Horizontal"));
            _animator.SetFloat(VerticalF, Input.GetAxisRaw("Vertical"));
        }
        else
        {
            if (Math.Abs(_navMeshAgent.velocity.magnitude) > 0)
            {
                _animator.SetFloat(HorizontalF, 0);
                _animator.SetFloat(VerticalF, 1);
            }
            else
            {
                _animator.SetFloat(HorizontalF, 0);
                _animator.SetFloat(VerticalF, 0);
            }
                
        }*/
    }

    public void SetPickUpTrigger()
    {
        _animator.SetTrigger(PickUp);
        _animator.SetBool(HasItem, true);
    }

    public void SetDropTrigger()
    {
        _animator.SetBool(HasItem, false);
    }

    public void SetSlapTrigger()
    {
        _animator.SetFloat(Index,Random.Range(0,3));
        _animator.SetTrigger(Slap);
    }

    public void SetSlappedTrigger()
    {
        _animator.SetFloat(Index,Random.Range(0,1));
        _animator.SetTrigger(Slapped);
    }
    
}
