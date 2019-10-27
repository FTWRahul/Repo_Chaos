using UnityEngine;
using UnityEngine.Events;

public class Events
{
    //Game Manager
    [System.Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { }
    [System.Serializable] public class EventQuestMenuMove : UnityEvent { }
    //Input system events
    [System.Serializable] public class EventItemSelection : UnityEvent <GameObject> { }
    [System.Serializable] public class EventPickupCall : UnityEvent { }
    [System.Serializable] public class EventSlapCall : UnityEvent { }
    
    //Pickup
    [System.Serializable] public class EventItemPickup : UnityEvent { }
    [System.Serializable] public class EventItemDrop : UnityEvent { }
    [System.Serializable] public class EventPickupVariableChange : UnityEvent <bool> { }
    
    //Slap
    [System.Serializable] public class EventCharacterSlap : UnityEvent { }
    [System.Serializable] public class EventCharacterSlapped : UnityEvent { }
    [System.Serializable] public class EventCharacterEndSlap : UnityEvent { }
    
    //Animation
    [System.Serializable] public class EventUpdateMovement : UnityEvent <float, float> { }
    
    //Quest
    [System.Serializable] public class EventQuestDone : UnityEvent { }
    [System.Serializable] public class EventQuestLost : UnityEvent { }
    
    //Exit
    [System.Serializable] public class EventRightItem : UnityEvent { }
    [System.Serializable] public class EventWrongItem : UnityEvent { }
    [System.Serializable] public class EventItemRemoved : UnityEvent { }
}
