using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    private AgentBehaviour _agentBehaviour;
    private PickupSystem _pickupSystem;
    private SlapSystem _slapSystem;
    private CharacterAnimation _animation;
    private CharacterAudio _characterAudio;

    private void Start()
    {
        _agentBehaviour = GetComponent<AgentBehaviour>();
        _pickupSystem = GetComponent<PickupSystem>();
        _slapSystem = GetComponent<SlapSystem>();
        _animation = GetComponent<CharacterAnimation>();
        _characterAudio = GetComponentInChildren<CharacterAudio>();
        
        AddListeners();
    }

    private void AddListeners()
    {
        //Input system
        _agentBehaviour.onItemSelection.AddListener(_pickupSystem.SelectItem);
        _agentBehaviour.onPickupCall.AddListener(_pickupSystem.OnPickUpCalled);
        _agentBehaviour.onSlapCall.AddListener(_slapSystem.OnSlapCalled);
        
        //Update movement animation from inputs
        _agentBehaviour.onUpdateMovement.AddListener(_animation.UpdateMovement);
        
        //Pickup events
        _pickupSystem.onItemPickup.AddListener(_animation.SetPickUpTrigger);
        _pickupSystem.onItemDrop.AddListener(_animation.SetDropTrigger);
        _pickupSystem.onPickupVariableChange.AddListener(_slapSystem.ChangeSlapBool);

        //Slap event
        _slapSystem.onCharacterSlap.AddListener(_animation.SetSlapTrigger);
        _slapSystem.onCharacterSlapped.AddListener(_animation.SetSlappedTrigger);
        _slapSystem.onCharacterSlapped.AddListener(_agentBehaviour.DisableNavMesh);
        _slapSystem.onCharacterSlapped.AddListener(_characterAudio.PlaySlap);
        _slapSystem.onCharacterSlapped.AddListener(_pickupSystem.DropItem);
        _slapSystem.onCharacterEndSlap.AddListener(_agentBehaviour.LookForItem);
        _slapSystem.onCharacterEndSlap.AddListener(_agentBehaviour.EnableNavMesh);
    }
}
