using UnityEngine;
using System.Collections;

public class NPCUIFix : NPCUI
{

    void Start()
    {
        SetID(ID);
    }

    public override void SetID(int id)
    {
        base.SetID(id);
    }

}
