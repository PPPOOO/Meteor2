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


    private SpriteRenderer mSR;
    private Color mNormalColor;

    public bool IsDead = false;
    public AudioClip TakeDamageClip;

    protected GameObject HUDPrefab;
    protected Transform HUDPanel;



    public virtual void  Start()
    {
        TakeDamageClip = Resources.Load<AudioClip>("Sounds/slime-hit");
        mSR = GetComponent<SpriteRenderer>();
        mNormalColor = mSR.material.color;
    }
    public void HPChange(int count)
    {
        HP += count;
    }


    public virtual void HPRemainChange(int count)
    {
        HP_Remain += count;
        
        HUDShowDamageValue(count.ToString());
        if (count < 0)
        {
            if (IsDead == true) return;
            
            AudioSource.PlayClipAtPoint(TakeDamageClip, transform.position);
            StartCoroutine(ShowBodyRed());
        }
    }

    IEnumerator ShowBodyRed()
    {
        mSR.material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        mSR.material.color = mNormalColor;
    }
    public void HUDShowDamageValue(string des)
    {
        HUDPrefab = Resources.Load<GameObject>("HUDText");
        HUDPanel = GameObject.FindGameObjectWithTag("Canvas").transform.Find("HUDPanel");
        GameObject HUDTextgo = Instantiate(HUDPrefab);
        HUDTextgo.transform.SetParent(HUDPanel, false);
        HUDTextgo.GetComponent<HUDText>().target = gameObject.transform;
        HUDTextgo.GetComponent<HUDText>().ShowDamgeValue(des);
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
        AttackRate *= ((float)(100 + count)) / 100;
    }


    public void MoveSpeedChange(int count)
    {
        MoveSpeed *=((float) (100 + count)) / 100;
    }
}
