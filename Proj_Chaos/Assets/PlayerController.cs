using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputSystem _inputSystem;
    private MovementInput _movementInput;
    private PickupSystem _pickupSystem;
    private SlapSystem _slapSystem;
    private QuestGenerator _questGenerator; //why reference this one?
    private CharacterAnimation _animation;
    private CharacterAudio _characterAudio;
    private ExitCheck[] _exitChecks;

    private void Start()
    {
        _inputSystem = GetComponent<InputSystem>();
        _movementInput = GetComponent<MovementInput>();
        _pickupSystem = GetComponent<PickupSystem>();
        _slapSystem = GetComponent<SlapSystem>();
        _questGenerator = GetComponent<QuestGenerator>();
        _animation = GetComponent<CharacterAnimation>();
        _characterAudio = GetComponentInChildren<CharacterAudio>();
        _exitChecks = FindObjectsOfType<ExitCheck>();

        AddListeners();
    }

    private void AddListeners()
    {
        //Input system
        _inputSystem.onItemSelection.AddListener(_pickupSystem.SelectItem);
        _inputSystem.onPickupCall.AddListener(_pickupSystem.OnPickUpCalled);
        _inputSystem.onSlapCall.AddListener(_slapSystem.OnSlapCalled);
        
        //Update movement animation from inputs
        _movementInput.onUpdateMovement.AddListener(_animation.UpdateMovement);
        
        //Pickup events
        _pickupSystem.onItemPickup.AddListener(_animation.SetPickUpTrigger);
        _pickupSystem.onItemPickup.AddListener(_characterAudio.PlayPickUp);
        _pickupSystem.onItemDrop.AddListener(_animation.SetDropTrigger);
        _pickupSystem.onPickupVariableChange.AddListener(_slapSystem.ChangeSlapBool);

        //Slap event
        _slapSystem.onCharacterSlap.AddListener(_animation.SetSlapTrigger);
        _slapSystem.onCharacterSlapped.AddListener(_animation.SetSlappedTrigger);
        _slapSystem.onCharacterSlapped.AddListener(_characterAudio.PlaySlap);
        _slapSystem.onCharacterSlapped.AddListener(_pickupSystem.DropItem);

        //Audio to exit check
        foreach (var exitCheck in _exitChecks)
        {
            exitCheck.onWrongItem.AddListener(_characterAudio.PlayWrong);
            exitCheck.onRightItem.AddListener(_characterAudio.PlayMoney);
            exitCheck.onItemRemoved.AddListener(_pickupSystem.DropItem);
            //add onitem remove check for winning loose
        }
    }
}
