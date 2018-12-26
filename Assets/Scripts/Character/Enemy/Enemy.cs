using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy 
{
    public int ID;
    public string Name;
    public string Animation;
    public int Gold;
    public int Exp;
    public List<ApplyAttrEffect> ApplyAttrEffects;
    public Enemy(int id,string name,string animation,int gold,int exp,List<ApplyAttrEffect> applyAttrEffects)
    {
        ID = id;
        Name = name;
        Animation = animation;
        Gold = gold;
        Exp = exp;
        ApplyAttrEffects = applyAttrEffects;
    }

}
