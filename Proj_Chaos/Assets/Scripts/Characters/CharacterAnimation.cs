using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class CharacterAnimation : MonoBehaviour
{
    private Animator _animator;
    private PickUp _pickUp;
    private Slapper _slapper;
    private NavMeshAgent _navMeshAgent;
    private CharacterController _characterController;
    private CharacterAudio _characterAudio;
    
    private static readonly int HorizontalF = Animator.StringToHash("Horizontal_f");
    private static readonly int VerticalF = Animator.StringToHash("Vertical_f");
    private static readonly int Index = Animator.StringToHash("Index");
    private static readonly int Drop = Animator.StringToHash("Drop");
    private static readonly int PickUp = Animator.StringToHash("PickUp");
    private static readonly int Slap = Animator.StringToHash("Slap");
    private static readonly int Slapped = Animator.StringToHash("Slapped");
    private static readonly int HasItem = Animator.StringToHash("HasItem");

    public void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _pickUp = GetComponent<PickUp>();
        _slapper = GetComponent<Slapper>();

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
        _pickUp.onCharacterPickup.AddListener(SetPickUpTrigger);
        _pickUp.onHasItemChange.AddListener(SetItemBool);
        
        _slapper.OnCharacterSlap.AddListener(SetSlapTrigger);
        _slapper.OnCharacterSlapped.AddListener(SetSlappedTrigger);
    }

    public void OnDisable()
    {
        _pickUp.onCharacterPickup.RemoveListener(SetPickUpTrigger);
        _pickUp.onHasItemChange.RemoveListener(SetItemBool);
        
        _slapper.OnCharacterSlap.RemoveListener(SetSlapTrigger);
        _slapper.OnCharacterSlapped.RemoveListener(SetSlappedTrigger);
    }

    private void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (_characterController)
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
                
        }
    }
    

    void SetItemBool()
    {
        _animator.SetBool(HasItem, _pickUp.hasItem);
    }
    
    void SetPickUpTrigger()
    {
        _animator.SetTrigger(PickUp);
    }

    void SetSlapTrigger()
    {
        _animator.SetFloat(Index,Random.Range(0,3));
        _animator.SetTrigger(Slap);
    }

    void SetSlappedTrigger()
    {
        _animator.SetFloat(Index,Random.Range(0,1));
        _animator.SetTrigger(Slapped);
    }
    
}
