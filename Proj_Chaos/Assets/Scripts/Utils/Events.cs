using UnityEngine.Events;

public class Events
{
    [System.Serializable] public class EventFadeComplete : UnityEvent<bool> { }
    
    [System.Serializable] public class EventMoveList : UnityEvent<bool> { }
    
    
    [System.Serializable] public class EventEPressed : UnityEvent { }
    
    [System.Serializable] public class EventLMBPressed : UnityEvent { }
    
    [System.Serializable] public class EventCharacterSlapped : UnityEvent { }
    
    
    [System.Serializable] public class EventCharacterSlap : UnityEvent { }
    
    [System.Serializable] public class EventCharacterAbleSlap : UnityEvent { }
    
    
    [System.Serializable] public class EventCharacterPickup : UnityEvent { }
    
    [System.Serializable] public class EventCharacterDrop : UnityEvent { }
    
    [System.Serializable] public class EventCharacterHasItemChange : UnityEvent { }
    
    [System.Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { }
    
    [System.Serializable] public class EventRightItem : UnityEvent { }
    
    [System.Serializable] public class EventWrongItem : UnityEvent { }
    [System.Serializable] public class EventItemRemoved : UnityEvent { }
    
}