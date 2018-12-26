using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ApplyAttrEffect 
{
    public AttrType AT;
    public bool Positive;
    public int Count;
    public float Time;
    public float FixValue;
    public List<AddAttrValue> AddAttrValues = new List<AddAttrValue>();

    public ApplyAttrEffect(AttrType attrType, bool positive, float fixValue)
    {
        AT = attrType;
        Positive = positive;
        FixValue = fixValue;
    }

    public ApplyAttrEffect(AttrType attrType,  float fixValue)
    {
        AT = attrType;
        FixValue = fixValue;
    }

    public ApplyAttrEffect(AttrType attrType,float fixValue, List<AddAttrValue> addAttrValues,float time= 0, int count = 1)
    {
        AT = attrType;
        FixValue = fixValue;
        AddAttrValues = addAttrValues;
        Time = time;
        Count = count;
    }
}
