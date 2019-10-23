using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PickUp _pickUp;
    private Slapper _slapper;
    
    private static readonly int Velocity = Animator.StringToHash("Velocity_i");
    private static readonly int HorizontalF = Animator.StringToHash("Horizontal_f");
    private static readonly int VerticalF = Animator.StringToHash("Vertical_f");
    private static readonly int Drop = Animator.StringToHash("Drop");
    private static readonly int PickUp = Animator.StringToHash("PickUp");
    private static readonly int Slap = Animator.StringToHash("Slap");
    private static readonly int Slapped = Animator.StringToHash("Slapped");
    private static readonly int HasItem = Animator.StringToHash("HasItem");

    public void Awake()
    {
        animator = GetComponent<Animator>();
        _pickUp = GetComponent<PickUp>();
        _slapper = GetComponent<Slapper>();
    }
    
    public void OnEnable()
    {
         //UpdateAnimation every frame
         //_pickUp. set item bool && Set pick up trigger && set drop trigger
         // slapper setSlapTrigger onSlapEvent && setSlappedTrigger onSlappedEvent 
    }
    
    void UpdateAnimation(float magnitude, float x, float z)
    {
        //update info every frame devend on charactercontroller or navmesh
        animator.SetFloat(Velocity, (int)magnitude);
        animator.SetFloat(HorizontalF, x);
        animator.SetFloat(VerticalF, z);
    }
    
    void SetDropTrigger()
    {
        animator.SetTrigger(Drop);
    }

    void SetItemBool(bool hasItem)
    {
        animator.SetBool(HasItem, hasItem);
    }
    
    void SetPickUpTrigger()
    {
        animator.SetTrigger(PickUp);
    }

    void SetSlapTrigger()
    {
        animator.SetTrigger(Slap);
    }

    void SetSlappedTrigger()
    {
        animator.SetTrigger(Slapped);
    }
    
}
