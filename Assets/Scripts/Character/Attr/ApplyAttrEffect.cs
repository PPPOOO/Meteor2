using UnityEngine;
using System.Collections;

public class ApplyAttrEffect 
{
    public AttrType AT;
    public bool Positive;
    public float Time;
    public float FixValue;

    public ApplyAttrEffect(AttrType attrType, bool positive, float fixValue)
    {
        AT = attrType;
        Positive = positive;
        FixValue = fixValue;
    }

}
