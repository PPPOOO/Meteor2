using UnityEngine;
using System.Collections;

public class NewDayObserverBusinessManager : IGameEventObserver
{
    private NewDaySubject NewDaySubject;
    private BusinessManager BusinessManager;

    public NewDayObserverBusinessManager(BusinessManager businessManager)
    {
        BusinessManager = businessManager;
    }

    public override void SetSubject(IGameEventSubject sub)
    {
        NewDaySubject = sub as NewDaySubject;
    }

    public override void Update()
    {
        BusinessManager.NewDayCome();
    }
}
