using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacetStatus : MonoBehaviour {

    public int Level;
    public int HP ;
    public int HP_Remain ;
    public int MP ;
    public int MP_Remain ;
    public int EP ;
    public int EP_Remain ;
    public int Hunger ;
    public int Hunger_Remain ;
    public int STR ;
    public int AGI ;
    public int MAG ;
    public int VIT ;

    public int AD;
    public float AttackDistance=2;
    public float AttackRate=1;
    public float AttackTimer=0;
    public float MoveSpeed ;

    public void HPChange(int count)
    {
        HP += count;
    }


    public void HPRemainChange(int count)
    {
        HP_Remain += count;
    }

    public void MPChange(int count)
    {
        MP += count;
    }
    public void MPRemainChange(int count)
    {
        MP_Remain += count;
    }

    public void EPChange(int count)
    {
        EP += count;
    }
    public void EPRemainChange(int count)
    {
        EP_Remain += count;
    }


    public void HungerRemainChange(int count)
    {
        Hunger_Remain += count;
    }

    public void ADChange(int count)
    {
        AD += count;
    }

    public void AttackRateChange(int count)
    {
        AttackRate *= ((float)count) / 100;
    }

}
