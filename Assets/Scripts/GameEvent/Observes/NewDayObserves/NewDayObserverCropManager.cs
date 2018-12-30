using UnityEngine;
using System.Collections;

public class NewDayObserverCropManager : IGameEventObserver
{
    private CropManager CM;
    private NewDaySubject NewDaySubject;
    public NewDayObserverCropManager(CropManager cm)
    {
        CM = cm;
    }
    public override void SetSubject(IGameEventSubject sub)
    {
        NewDaySubject = sub as NewDaySubject;
    }

    public override void Update()
    {
        CM.NewDayCome();
    }


}
