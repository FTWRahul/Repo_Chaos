﻿using UnityEngine.Events;

public class Events
{
    [System.Serializable] public class EventFadeComplete : UnityEvent<bool> { }
    
    [System.Serializable] public class EventEPressed : UnityEvent { }
    
    [System.Serializable] public class EventLMBPressed : UnityEvent { }
    
    [System.Serializable] public class EventCharacterSlapped : UnityEvent { }
    
    
    [System.Serializable] public class EventCharacterAbleSlap : UnityEvent { }
    
    [System.Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { }
}