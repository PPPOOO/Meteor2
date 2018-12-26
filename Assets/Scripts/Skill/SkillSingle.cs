using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSingle : SkillBaseInfo
{
    public float Distance;
    public float Value;
    public List<ApplyAttrEffect> ApplyAttrEffects = new List<ApplyAttrEffect>();

    public SkillSingle(int id, string name, string sprite, string des, int mp, int ep, int lv, float coolTime, ReleaseType releaseType,
           float distance, List<ApplyAttrEffect> applyAttrEffects) 
        : base(id, name, sprite, des, mp, ep, lv, coolTime, releaseType)
    {
        Distance = distance;
        ApplyAttrEffects = applyAttrEffects;
    }
}