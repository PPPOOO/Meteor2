using UnityEngine;
using System.Collections;

public class ItemGetSubject : IGameEventSubject
{

    public int ItemID;
    public ItemGetSubject(int id)
    {
        ItemID = id;
    }

    public override void Notify()
    {

        base.Notify();
    }
}
