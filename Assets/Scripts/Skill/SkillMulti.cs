using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMulti : SkillBaseInfo
{
    public float Range;
    public float Distance;
    public float Value;

    public SkillMulti(int id, string name, string sprite, string des, int mp, int ep, int lv, float coolTime,ReleaseObject releaseObject, ReleaseType releaseType,
           float range,float distance , List<ApplyAttrEffect> applyAttrEffects) 
        : base(id, name, sprite, des, mp, ep, lv, coolTime,  releaseObject, releaseType, applyAttrEffects)
    {
        Range = range;
        Distance = distance;
    }
}
