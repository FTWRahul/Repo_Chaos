using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PickUp _pickUp;
    private Slapper _slapper;
    private NavMeshAgent _navMeshAgent;
    private CharacterController _characterController;
    
    private static readonly int HorizontalF = Animator.StringToHash("Horizontal_f");
    private static readonly int VerticalF = Animator.StringToHash("Vertical_f");
    private static readonly int Index = Animator.StringToHash("Index");
    private static readonly int Drop = Animator.StringToHash("Drop");
    private static readonly int PickUp = Animator.StringToHash("PickUp");
    private static readonly int Slap = Animator.StringToHash("Slap");
    private static readonly int Slapped = Animator.StringToHash("Slapped");
    private static readonly int HasItem = Animator.StringToHash("HasItem");

    public void Start()
    {
        animator = GetComponentInChildren<Animator>();
        _pickUp = GetComponent<PickUp>();

        _pickUp.onCharacterPickup.AddListener(SetPickUpTrigger);
        _pickUp.onHasItemChange.AddListener(SetItemBool);
        
        _slapper = GetComponent<Slapper>();
        
        _slapper.OnCharacterSlap.AddListener(SetSlapTrigger);
        _slapper.OnCharacterSlapped.AddListener(SetSlappedTrigger);
        
        if (GetComponent<CharacterController>())
        {
            _characterController = GetComponent<CharacterController>();
        }
        else
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }
    }
    
    public void OnEnable()
    {
        //_pickUp. set item bool && Set pick up trigger && set drop trigger
        
    }

    private void Update()
    {
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        if (_characterController)
        {
            animator.SetFloat(HorizontalF, Input.GetAxisRaw("Horizontal"));
            animator.SetFloat(VerticalF, Input.GetAxisRaw("Vertical"));
        }
        else
        {
            if (Math.Abs(_navMeshAgent.velocity.magnitude) > 0)
            {
                animator.SetFloat(HorizontalF, 0);
                animator.SetFloat(VerticalF, 1);
            }
            else
            {
                animator.SetFloat(HorizontalF, 0);
                animator.SetFloat(VerticalF, 0);
            }
                
        }
    }
    
    void SetDropTrigger()
    {
        animator.SetTrigger(Drop);
    }

    void SetItemBool()
    {
        animator.SetBool(HasItem, _pickUp.hasItem);
    }
    
    void SetPickUpTrigger()
    {
        animator.SetTrigger(PickUp);
    }

    void SetSlapTrigger()
    {
        animator.SetFloat(Index,Random.Range(0,3));
        animator.SetTrigger(Slap);
    }

    void SetSlappedTrigger()
    {
        animator.SetTrigger(Slapped);
    }
    
}
