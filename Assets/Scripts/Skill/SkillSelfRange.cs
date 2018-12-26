using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelfRange : SkillBaseInfo
{
    public float Range;
    public List<ApplyAttrEffect> ApplyAttrEffects = new List<ApplyAttrEffect>();

    public SkillSelfRange(int id, string name, string sprite, string des, int mp, int ep, int lv, float coolTime, ReleaseType releaseType,
           float range, List<ApplyAttrEffect> applyAttrEffects)
        : base(id, name, sprite, des, mp, ep, lv,  coolTime, releaseType)
    {
        Range = range;
        ApplyAttrEffects = applyAttrEffects;
    }
}