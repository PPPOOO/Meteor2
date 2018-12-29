using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelfRange : SkillBaseInfo
{
    public float Range;

    public SkillSelfRange(int id, string name, string sprite, string des, int mp, int ep, int lv, float coolTime, ReleaseObject releaseObject, ReleaseType releaseType,
           float range, List<ApplyAttrEffect> applyAttrEffects)
        : base(id, name, sprite, des, mp, ep, lv,  coolTime,  releaseObject, releaseType, applyAttrEffects)
    {
        Range = range;
    }
}