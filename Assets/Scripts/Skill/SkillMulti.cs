using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMulti : SkillBaseInfo
{
    public float Range;
    public float Distance;
    public float Value;
    public List<ApplyAttrEffect> ApplyAttrEffects = new List<ApplyAttrEffect>();

    public SkillMulti(int id, string name, string sprite, string des, int mp, int ep, int lv, float coolTime, ReleaseType releaseType,
           float range,float distance , List<ApplyAttrEffect> applyAttrEffects) 
        : base(id, name, sprite, des, mp, ep, lv, coolTime, releaseType)
    {
        Range = range;
        Distance = distance;
        ApplyAttrEffects = applyAttrEffects;
    }
}
