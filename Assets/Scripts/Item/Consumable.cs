using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Consumable : Item
{
    
    public List< ApplyAttrEffect> ApplyAttrEffects;

    public Consumable(int id, string name, string des, string sprite, int buyprice, int sellpirce, int capacity, ItemType type,
          List<ApplyAttrEffect> applyAttrEffects)
        : base(id, name, des, sprite, buyprice, sellpirce, capacity, type)
    {
        ApplyAttrEffects = applyAttrEffects;
    }


    public override string GetToolTipText()
    {
        string attreffect = "";
        foreach (ApplyAttrEffect applyAttrEffect in ApplyAttrEffects)
        {
            string effect = AttrManager.Instance.GetAttrChineseNameByTpye(applyAttrEffect.AT) + ":" + applyAttrEffect.FixValue + "\n";
            attreffect += effect;
        }
        return base.GetToolTipText()+attreffect;
    }

}
