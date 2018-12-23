using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : CharacetStatus {

    public string Name = "默认名称";


    //public float MP = 100;
    //public float Energy = 100;
    //public float Hunger = 100;
    
    //public float MP_Remain = 100;
    //public float Energy_Remain = 100;
    //public float Hunger_Remain = 100;

    public int Exp = 0;
    public int Total_exp = 100;
    
    public int Strength_plus = 0;
    public int Agility_plus = 0;
    public int Magic_plus = 0;
    public int Vitality_plus = 0;
    public int Point_remain = 10;
    
    public int HungerSpeed = 1;


    public int CoinCount = 1000;

    private void Start()
    {
        InvokeRepeating("InHunger", 0, 2);
        STR = 20;
        AGI = 20;
        MAG = 20;
        VIT = 20;
    }

    public void GetHeal(int hp, int mp)
    {
        HP_Remain += hp;
        HP_Remain = Mathf.Clamp(HP_Remain, 0, HP);

        MP_Remain += mp;
        MP_Remain = Mathf.Clamp(MP_Remain, 0, MP);
    }
    
    #region 四个基础属性的加点 Plusxx()
    public void PlusStrength()
    {
        if (Point_remain >= 1)
        {
            Strength_plus++;
            Point_remain -= 1;
            StatusPanel.Instance.UpdateStatusPanel();
        }
    }
    public void PlusAgility()
    {
        if (Point_remain >= 1)
        {
            Agility_plus++;
            Point_remain -= 1;
            StatusPanel.Instance.UpdateStatusPanel();
        }
    }
    public void PlusMagic()
    {
        if (Point_remain >= 1)
        {
            Magic_plus++;
            Point_remain -= 1;
            StatusPanel.Instance.UpdateStatusPanel();
        }
    }
    public void PlusVitality()
    {
        if (Point_remain >= 1)
        {
            Vitality_plus++;
            Point_remain -= 1;
            StatusPanel.Instance.UpdateStatusPanel();
        }
    }
    #endregion

    public void GetExp(int exp)
    {
        Exp += exp;
        Total_exp = 100 + Level * 30;
        if (Exp > Total_exp)
        {
            Level++;
            Exp -= Total_exp;
            Total_exp = 100 + Level * 30;
        }
    }

    public bool TakeMP(int count)
    {
        if (MP_Remain >= count)
        {
            MP_Remain -= count;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TakeEP(int count)
    {
        if (EP_Remain >= count)
        {
            EP_Remain -= count;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TakeHunger(int count = 1)
    {
        if (Hunger_Remain >= count)
        {
            Hunger_Remain -= count;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void InHunger()
    {
        Hunger_Remain -= HungerSpeed;
    }

    public void CoinUP(int count)
    {
        CoinCount += count;
        Bag_PutOnPanel.Instance.UpdateCoin();
    }
    public void CoinDown(int count)
    {
        CoinCount -= count;
        Bag_PutOnPanel.Instance.UpdateCoin();
    }
    public bool TakeCoin(int count)
    {
        if (CoinCount >= count)
        {
            CoinDown(count);
            return true;
        }
        else return false;
    }
}
