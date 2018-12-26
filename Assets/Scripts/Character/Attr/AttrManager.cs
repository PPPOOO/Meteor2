using UnityEngine;
using System.Collections;

public enum AttrType
{
    None,
    AD,
    HP,
    HPRemain,
    MP,
    MPRemain,
    EP,
    EPRemain,
    Hunger,
    HungerRemain,
    STR,
    AGI,
    MAG,
    VIT,
    AttackRate
}


public class AttrManager : MonoBehaviour
{
    private static AttrManager _instance;
    public static AttrManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag("Manager").GetComponent<AttrManager>();
            }
            return _instance;
        }
    }

    public float GetAttrByType(GameObject gameObject, AttrType attrType)
    {
        CharacetStatus status = gameObject.GetComponent<CharacetStatus>();
        if (attrType == AttrType.STR)
        {
            return status.STR;
        }
        if (attrType == AttrType.AGI)
        {
            return status.AGI;
        }
        if (attrType == AttrType.AttackRate)
        {
            return status.AttackRate;
        }
        else
        {
            Debug.LogError("没有对应属性的类型");
            return 0;
        }
    }

    

    public void ChangeAttrByType(GameObject gameObject,ApplyAttrEffect applyAttrEffect)
    {
        CharacetStatus status = gameObject.GetComponent<CharacetStatus>();
        if (applyAttrEffect.AT == AttrType.HP)
        {
            status.HPChange((int)applyAttrEffect.FixValue);
        }
        else if (applyAttrEffect.AT == AttrType.HPRemain)
        {
            status.HPRemainChange((int)applyAttrEffect.FixValue);
        }
        else if (applyAttrEffect.AT == AttrType.MP)
        {
            status.MPChange((int)applyAttrEffect.FixValue);
        }
        else if (applyAttrEffect.AT == AttrType.MPRemain)
        {
            status.MPRemainChange((int)applyAttrEffect.FixValue);
        }
        else if (applyAttrEffect.AT == AttrType.EP)
        {
            status.EPChange((int)applyAttrEffect.FixValue);
        }
        else if (applyAttrEffect.AT == AttrType.EPRemain)
        {
            status.EPRemainChange((int)applyAttrEffect.FixValue);
        }
        else if (applyAttrEffect.AT == AttrType.HungerRemain)
        {
            status.HungerRemainChange((int)applyAttrEffect.FixValue);
        }else if (applyAttrEffect.AT == AttrType.AD)
        {
            status.ADChange((int)applyAttrEffect.FixValue);
        }
        else if (applyAttrEffect.AT == AttrType.AttackRate)
        {
            status.AttackRateChange((int)applyAttrEffect.FixValue);
        }
    }




    public string GetAttrChineseNameByTpye(AttrType attrType)
    {
        string chsname = " ";
        switch (attrType)
        {
            case AttrType.None:
                break;
            case AttrType.AD:
                chsname = "攻击力";
                break;
            case AttrType.HP:
                break;
            case AttrType.HPRemain:
                chsname = "血量";
                break;
            case AttrType.MP:
                break;
            case AttrType.MPRemain:
                chsname = "魔量";
                break;
            case AttrType.EP:
                break;
            case AttrType.EPRemain:
                chsname = "能量";
                break;
            case AttrType.Hunger:
                break;
            case AttrType.HungerRemain:
                chsname = "饥饿";
                break;
            case AttrType.STR:
                chsname = "力量";
                break;
            case AttrType.AGI:
                chsname = "敏捷";
                break;
            case AttrType.MAG:
                chsname = "魔力";
                break;
            case AttrType.VIT:
                chsname = "体质";
                break;
            case AttrType.AttackRate:
                break;
            default:
                break;
        }
        return chsname;
    }
}
