using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameEventType
{
    Null,
    NewDay
}
public class GameEventManager : MonoSingleton<GameEventManager>
{


    private Dictionary<GameEventType, IGameEventSubject> mGameEvents = new Dictionary<GameEventType, IGameEventSubject>();

    protected override void Awake()
    {
        base.Awake();
    }

    private IGameEventSubject GetGameEvent(GameEventType eventType)
    {
        return mGameEvents[eventType];
    }


    public void RegisterObserver(GameEventType eventType, IGameEventObserver observer)
    {
        IGameEventSubject sub = GetGameEvent(eventType);
        if (sub == null) return;
        sub.RegisterObserver(observer);
        observer.SetSubject(sub);
    }
    public void RemoveObserver(GameEventType eventType, IGameEventObserver observer)
    {
        IGameEventSubject sub = GetGameEvent(eventType);
        if (sub == null) return;
        sub.RemoveObserver(observer);
        observer.SetSubject(null);
    }



    public void NotifySubject(GameEventType eventType)
    {
        IGameEventSubject sub = GetGameEvent(eventType);
        if (sub == null) return;
        sub.Notify();
    }
}
