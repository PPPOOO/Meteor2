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
    AttackRate,
    MoveSpeed
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
        else if (attrType == AttrType.MAG)
        {
            return status.MAG;
        }
        else if (attrType == AttrType.VIT)
        {
            return status.VIT;
        }
        if (attrType == AttrType.AttackRate)
        {
            return status.AttackRate;
        }
        else if (attrType == AttrType.HPRemain)
        {
            return status.HP_Remain;
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

    public void ChangeAttrByType(GameObject gameObject, AttrType attrType,float value)
    {
        CharacetStatus status = gameObject.GetComponent<CharacetStatus>();
        if (attrType == AttrType.HP)
        {
            status.HPChange((int)value);
        }
        else if (attrType == AttrType.HPRemain)
        {
            status.HPRemainChange((int)value);
        }
        else if (attrType == AttrType.MP)
        {
            status.MPChange((int)value);
        }
        else if (attrType == AttrType.MPRemain)
        {
            status.MPRemainChange((int)value);
        }
        else if (attrType == AttrType.EP)
        {
            status.EPChange((int)value);
        }
        else if (attrType == AttrType.EPRemain)
        {
            status.EPRemainChange((int)value);
        }
        else if (attrType == AttrType.HungerRemain)
        {
            status.HungerRemainChange((int)value);
        }
        else if (attrType == AttrType.AD)
        {
            status.ADChange((int)value);
        }
        else if (attrType == AttrType.AttackRate)
        {
            status.AttackRateChange((int)value);
        }
        else if (attrType == AttrType.MoveSpeed)
        {
            status.MoveSpeedChange((int)value);
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
