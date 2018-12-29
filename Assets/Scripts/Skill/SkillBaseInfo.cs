using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBaseInfo
{
    public int ID;
    public string Name;
    public string Sprite;
    public string Des;
    public int MP;
    public int EP;
    public int DemandLv;
    public float CoolTime;
    public ReleaseObject Releaseobject;
    public ReleaseType Type;
    public List<ApplyAttrEffect> ApplyAttrEffects = new List<ApplyAttrEffect>();


    public SkillBaseInfo(int id,string name,string sprite,string des,int mp,int ep,int lv,float coolTime,ReleaseObject releasetype,ReleaseType releaseType, List<ApplyAttrEffect> applyAttrEffects)
    {
        ID = id;
        Name = name;
        Sprite = sprite;
        Des = des;
        MP = mp;
        EP = ep;
        DemandLv = lv;
        CoolTime = coolTime;
        Releaseobject = releasetype;
        Type = releaseType;
        ApplyAttrEffects = applyAttrEffects;
    }

    public enum ReleaseType
    {
        Self,
        SelfRange,
        Multi,
        Single,
        Trajectory
    }

    public enum ReleaseObject
    {
        Ally,
        Enemy
    }
    
}
