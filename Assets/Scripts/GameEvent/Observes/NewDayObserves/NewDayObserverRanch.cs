using UnityEngine;
using System.Collections;

public class NewDayObserverRanch : IGameEventObserver
{

    private NewDaySubject NewDaySubject;
    private RanchnManager ranchnManager;

    public NewDayObserverRanch(RanchnManager rm)
    {
        ranchnManager = rm;
    }

    public override void SetSubject(IGameEventSubject sub)
    {
        NewDaySubject = sub as NewDaySubject;
    }

    public override void Update()
    {
        ranchnManager.NewDayCome();
    }
}
