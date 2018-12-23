using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacetStatus : MonoBehaviour {

    public int Level;
    public int HP = 100;
    public int HP_Remain = 50;
    public int MP = 100;
    public int MP_Remain = 50;
    public int EP = 100;
    public int EP_Remain = 50;
    public int Hunger = 100;
    public int Hunger_Remain = 50;
    public int STR ;
    public int AGI ;
    public int MAG ;
    public int VIT ;


    public float AttackDistance;
    public float AttackRate;
    public float AttackTimer;
    public float MoveSpeed = 5;


    public void HPRemainUp(int count)
    {
        HP_Remain += count;
    }
    public void HPRemainDown(int count)
    {
        HP_Remain -= count;
    }

    public void MPRemainUp(int count)
    {
        MP_Remain += count;
    }
    public void MPRemainDown(int count)
    {
        MP_Remain -= count;
    }

    public void EPRemainUp(int count)
    {
        EP_Remain += count;
    }
    public void EPRemainDown(int count)
    {
        EP_Remain -= count;
    }

    public void HungerRemainUp(int count)
    {
        Hunger_Remain += count;
    }
    public void HungerRemainDown(int count)
    {
        Hunger_Remain -= count;
    }



    public void AttackRateUp(int count)
    {
        AttackRate *= ((float)count) / 100;
    }
    public void AttackRateDown(int count)
    {
        AttackRate /=((float) count) / 100;
    }

}
