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


    public void ChangeAttrByType(GameObject gameObject, AttrType attrType, bool ispositive, float value)
    {

        CharacetStatus status = gameObject.GetComponent<CharacetStatus>();
        int intvalue = (int)value;
        if (attrType == AttrType.HPRemain)
        {
            if (ispositive)
                status.HPRemainUp(intvalue);
            else
                status.HPRemainDown(intvalue);
        }
        if (attrType == AttrType.MPRemain)
        {
            if (ispositive)
                status.MPRemainUp(intvalue);
            else
                status.MPRemainDown(intvalue);
        }
        if (attrType == AttrType.EPRemain)
        {
            if (ispositive)
                status.EPRemainUp(intvalue);
            else
                status.EPRemainDown(intvalue);
        }
        if (attrType == AttrType.HungerRemain)
        {
            if (ispositive)
                status.HungerRemainUp(intvalue);
            else
                status.HungerRemainDown(intvalue);
        }
        

        if (attrType == AttrType.AttackRate)
        {
            if (ispositive)
                status.AttackRateUp(intvalue);
            else
                status.AttackRateDown(intvalue);
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
