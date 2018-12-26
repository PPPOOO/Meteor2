using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTrajectory : SkillBaseInfo
{
    public float ShotSize;
    public float ShotSpeed;
    public float ShotTime;
    public bool Pierce;
    public List<ApplyAttrEffect> ApplyAttrEffects = new List<ApplyAttrEffect>();

    public SkillTrajectory(int id, string name, string sprite, string des, int mp, int ep, int lv, float coolTime, ReleaseType releaseType,
           float shotsize, float shotspeed,float shottime, bool pierce, List<ApplyAttrEffect> applyAttrEffects) 
        : base(id, name, sprite, des, mp, ep, lv, coolTime, releaseType)
    {
        ShotSize = shotsize;
        ShotSpeed = shotspeed;
        ShotTime = shottime;
        Pierce = pierce;
        ApplyAttrEffects = applyAttrEffects;
    }
}