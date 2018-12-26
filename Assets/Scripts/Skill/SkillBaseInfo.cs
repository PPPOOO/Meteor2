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
    public ReleaseType Type;


    public SkillBaseInfo(int id,string name,string sprite,string des,int mp,int ep,int lv,float coolTime,ReleaseType releaseType)
    {
        ID = id;
        Name = name;
        Sprite = sprite;
        Des = des;
        MP = mp;
        EP = ep;
        DemandLv = lv;
        CoolTime = coolTime;
        Type = releaseType;
    }

    public enum ReleaseType
    {
        Self,
        SelfRange,
        Multi,
        Single,
        Trajectory
    }
    
}
