using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equipment : Item
{
    public enum EquipType
    {
        None,
        Hand,
        Head,
        Armour,
        Trimming
    }

    public EquipType Equiptype;
    public List<ApplyAttrEffect> ApplyAttrEffects;

    public Equipment(int id,string name,string des,string sprite,int buyprice,int sellpirce,int capacity,ItemType type,
        EquipType et, List<ApplyAttrEffect> applyAttrEffects)
        : base(id, name, des, sprite, buyprice, sellpirce, capacity, type)
    {
        Equiptype = et;
        ApplyAttrEffects = applyAttrEffects;
    }

    public override string GetToolTipText()
    {
        base.GetToolTipText();
        string equiptype = " ";
        switch (Equiptype)
        {
            case EquipType.None:
                break;
            case EquipType.Hand:
                equiptype = "武器";
                break;
            case EquipType.Head:
                equiptype = "头盔";
                break;
            case EquipType.Armour:
                equiptype = "盔甲";
                break;
            case EquipType.Trimming:
                equiptype = "饰品";
                break;
        }
        string attreffect = "";
        foreach (ApplyAttrEffect applyAttrEffect in ApplyAttrEffects)
        {
            string effect = AttrManager.Instance.GetAttrChineseNameByTpye(applyAttrEffect.AT)+":"+applyAttrEffect.FixValue+"\n";
            attreffect += effect;
        }
        string showtext = base.GetToolTipText() +equiptype + "\n" + attreffect;

        return showtext;
    }

}

