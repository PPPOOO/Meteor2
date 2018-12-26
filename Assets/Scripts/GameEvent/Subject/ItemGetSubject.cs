using UnityEngine;
using System.Collections;

public class ItemGetSubject : IGameEventSubject
{

    public int ItemCount=0;




    public override void Notify()
    {
        
        base.Notify();
    }
}
