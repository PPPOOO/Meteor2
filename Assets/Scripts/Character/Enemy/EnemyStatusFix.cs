using UnityEngine;
using System.Collections;

public class EnemyStatusFix : EnemyStatus
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
