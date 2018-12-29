using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTrajectory : SkillBaseInfo
{
    public float ShotSize;
    public float ShotSpeed;
    public float ShotTime;
    public bool Pierce;

    public SkillTrajectory(int id, string name, string sprite, string des, int mp, int ep, int lv, float coolTime, ReleaseObject releaseObject, ReleaseType releaseType,
           float shotsize, float shotspeed,float shottime, bool pierce, List<ApplyAttrEffect> applyAttrEffects) 
        : base(id, name, sprite, des, mp, ep, lv, coolTime,  releaseObject, releaseType, applyAttrEffects)
    {
        ShotSize = shotsize;
        ShotSpeed = shotspeed;
        ShotTime = shottime;
        Pierce = pierce;
    }
}