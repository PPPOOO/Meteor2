using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelf : SkillBaseInfo
{
    public List<ApplyAttrEffect> ApplyAttrEffects = new List<ApplyAttrEffect>();

    public SkillSelf(int id, string name, string sprite, string des, int mp, int ep, int lv,  float coolTime, ReleaseType releaseType,
          List<ApplyAttrEffect> applyAttrEffects) 
        : base(id, name, sprite, des, mp, ep, lv,  coolTime, releaseType)
    {
        ApplyAttrEffects = applyAttrEffects;
    }
}
