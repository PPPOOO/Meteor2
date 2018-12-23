using UnityEngine;
using System.Collections;

public class NPCUIFix : NPCUI
{

    // Use this for initialization
    void Start()
    {
        SetID(ID);
    }

    public override void SetID(int id)
    {
        base.SetID(id);
    }

}
