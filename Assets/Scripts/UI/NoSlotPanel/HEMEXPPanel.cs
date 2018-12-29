using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HEMEXPPanel : BasePanel<HEMEXPPanel>
{


    
    private Slider mHPSlider;
    private Text mHPText;
    private Slider mEPSlider;
    private Text mEPText;
    private Slider mMPSlider;
    private Text mMPText;
    private Slider mHungerSlider;
    private Text mHunerText;
    
    private Slider mExpSlider;
    private Text mExpText;

    private PlayerStatus mPlayerStatus;

    public override void Start()
    {
        mHPSlider = UITool.FindChild<Slider>(gameObject, "HPSlider");
        mHPText = UITool.FindChild<Text>(gameObject, "HP");
        mEPSlider = UITool.FindChild<Slider>(gameObject, "EPSlider");
        mEPText = UITool.FindChild<Text>(gameObject, "EP");
        mMPSlider = UITool.FindChild<Slider>(gameObject, "MPSlider");
        mMPText = UITool.FindChild<Text>(gameObject, "MP");
        mHungerSlider = UITool.FindChild<Slider>(gameObject, "HungerSlider");
        mHunerText = UITool.FindChild<Text>(gameObject, "Hunger");
        mExpSlider = UITool.FindChild<Slider>(gameObject, "ExpSlider");
        mExpText = UITool.FindChild<Text>(gameObject, "Exp");


        mPlayerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        UpdateShow();
    }


    private void Update()
    {
        UpdateShow();
    }

    public void UpdateShow()
    {
        mHPSlider.value =(float) mPlayerStatus.HP_Remain / mPlayerStatus.HP;
        mHPText.text = (mPlayerStatus.HP_Remain + "/" + mPlayerStatus.HP);

        mEPSlider.value = (float)mPlayerStatus.EP_Remain / mPlayerStatus.EP;
        mEPText.text = (mPlayerStatus.EP_Remain + "/"+ mPlayerStatus.EP);

        mMPSlider.value = (float)mPlayerStatus.MP_Remain / mPlayerStatus.MP;
        mMPText.text = (mPlayerStatus.MP_Remain + "/" + mPlayerStatus.MP);

        mHungerSlider.value = (float)mPlayerStatus.Hunger_Remain / mPlayerStatus.Hunger;
        mHunerText.text = (mPlayerStatus.Hunger_Remain + "/" + mPlayerStatus.Hunger);

        mExpSlider.value = ((float)mPlayerStatus.Exp) / mPlayerStatus.Total_exp;
        mExpText.text = (mPlayerStatus.Exp + "/" + mPlayerStatus.Total_exp);
    }
}
