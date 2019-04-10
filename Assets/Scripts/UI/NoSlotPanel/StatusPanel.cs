using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class StatusPanel : BasePanel<StatusPanel>
{


    private Text mName;
    private Text mLV;
    private Text mStrTt;
    private Button mStrBtn;
    private Text mAgiTt;
    private Button mAgiBtn;
    private Text mMagTt;
    private Button mMagBtn;
    private Text mVitTt;
    private Button mVitBtn;
    private Text mRemainPoint;
    
    private Text mDmgTt;
    private Text mHPTt;
    private Text mMPTt;
    private Text mEnergyTt;
    private Text mHungerTt;
    private Text mExp;

    private Button PreBtn;
    private Button NextBtn;
    private List<GameObject> PagePanels=new List<GameObject>();
    private int SelectPageIndex = 0;

    private PlayerStatus ps;


    public override void Start()
    {
        base.Start();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        GetPagePanels();
        GetUI();
        
        UpdatePageShow();
        InvokeRepeating("UpdateStatusPanel", 0, 1);
        
    }


    public void UpdateStatusPanel()
    {
        mName.text = ps.Name;
        mLV.text = ps.Level.ToString();
        mStrTt.text = ((int)(ps.STR + ps.Strength_plus) + PutOnPanel.Instance.Str).ToString();
        mAgiTt.text = ((int)(ps.AGI + ps.Agility_plus) + PutOnPanel.Instance.Agi).ToString();
        mMagTt.text = ((int)(ps.MAG + ps.Magic_plus) + PutOnPanel.Instance.Mag).ToString();
        mVitTt.text = ((int)(ps.VIT + ps.Vitality_plus) + PutOnPanel.Instance.Vit).ToString();
        //mStrTt.text = ps.STR.ToString();
        //mAgiTt.text = ps.AGI.ToString();
        //mMagTt.text = ps.MAG.ToString();
        //mVitTt.text = ps.VIT.ToString();
        mRemainPoint.text = ps.Point_remain.ToString();
        mDmgTt.text = PutOnPanel.Instance.Damage.ToString();
        mHPTt.text =  ps.HP_Remain.ToString();
        mMPTt.text =  ps.MP_Remain.ToString();
        mEnergyTt.text =  ps.EP_Remain.ToString();
        mHungerTt.text =  ps.Hunger_Remain.ToString();
        mExp.text = "经验 " + ps.Exp + "/" + ps.Total_exp;

        UpdateBtn();
    }


    public override void Show()
    {
        base.Show();
        UpdateStatusPanel();
    }

    private void UpdateBtn()
    {
        if (ps.Point_remain <= 0)
        {
            mStrBtn.GetComponent<Image>().enabled = false;
            mAgiBtn.GetComponent<Image>().enabled = false;
            mMagBtn.GetComponent<Image>().enabled = false;
            mVitBtn.GetComponent<Image>().enabled = false;
        }
        else
        {
            mStrBtn.GetComponent<Image>().enabled = true;
            mAgiBtn.GetComponent<Image>().enabled = true;
            mMagBtn.GetComponent<Image>().enabled = true;
            mVitBtn.GetComponent<Image>().enabled = true;
        }
    }

    private void GetUI()
    {
        mName = UITool.FindChild<Text>(gameObject, "Name");
        mLV = UITool.FindChild<Text>(gameObject, "LV");
        mStrTt = UITool.FindChild<Text>(gameObject, "STR");
        mStrBtn = UITool.GetButton(gameObject, "STRPlusBtn");
        mAgiTt = UITool.FindChild<Text>(gameObject, "AGI");
        mAgiBtn = UITool.GetButton(gameObject, "AGIPlusBtn");
        mMagTt = UITool.FindChild<Text>(gameObject, "MAG");
        mMagBtn = UITool.GetButton(gameObject, "MAGPlusBtn");
        mVitTt = UITool.FindChild<Text>(gameObject, "VIT");
        mVitBtn = UITool.GetButton(gameObject, "VITPlusBtn");

        mRemainPoint = UITool.FindChild<Text>(gameObject, "RemainPoint");
        mDmgTt = UITool.FindChild<Text>(gameObject, "Damage");
        mHPTt = UITool.FindChild<Text>(gameObject, "HP");
        mMPTt = UITool.FindChild<Text>(gameObject, "MP");
        mEnergyTt = UITool.FindChild<Text>(gameObject, "EP");
        mHungerTt = UITool.FindChild<Text>(gameObject, "Hunger");
        mExp = UITool.FindChild<Text>(gameObject, "Exp");

        PreBtn = UITool.FindChild<Button>(gameObject, "PreBtn");
        NextBtn = UITool.FindChild<Button>(gameObject, "NextBtn");

        mStrBtn.onClick.AddListener(ps.PlusStrength);
        mAgiBtn.onClick.AddListener(ps.PlusAgility);
        mMagBtn.onClick.AddListener(ps.PlusMagic);
        mVitBtn.onClick.AddListener(ps.PlusVitality);
        PreBtn.onClick.AddListener(OnPreBtnClick);
        NextBtn.onClick.AddListener(OnNextBtnClick);
    }

    public void GetPagePanels()
    {
        PagePanels.Add(transform.GetChild(0).gameObject);
        PagePanels.Add(transform.GetChild(1).gameObject);
        PagePanels.Add(transform.GetChild(2).gameObject);
    }

    public void UpdatePageShow()
    {
        PagePanels[SelectPageIndex].SetActive(true);
        for(int i = 0; i < PagePanels.Count; i++)
        {
            if (i != SelectPageIndex)
            {
                PagePanels[i].SetActive(false);
            }
        }
        
    }

    public void OnPreBtnClick()
    {
        SelectPageIndex--;
        if (SelectPageIndex == -1)
        {
            SelectPageIndex = PagePanels.Count - 1;
        }
        UpdatePageShow();
    }

    public void OnNextBtnClick()
    {
        SelectPageIndex++;
        SelectPageIndex %= PagePanels.Count;
        UpdatePageShow();
    }
}
