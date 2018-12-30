using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSBusinessPanel : BasePanel<MSBusinessPanel> {

    private static MSBusinessPanel _instance;
    public static MSBusinessPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag("Canvas").transform.Find("BusinessPanel/MSBusinessPanel").GetComponent<MSBusinessPanel>();
            }
            return _instance;
        }
    }

    private Button mPrayBtn;
    private Button mPolishBtn;
    private Button mBusinessBtn;
    private Text mPrayEP;
    private Text mPolishEP;

    public override void Start()
    {
        base.Start();
        mPrayBtn = UITool.GetButton(gameObject, "PrayBtn");
        mPolishBtn = UITool.GetButton(gameObject, "PolishBtn");
        mBusinessBtn = UITool.GetButton(gameObject, "BusinessBtn");
        mPrayEP = UITool.FindChild<Text>(gameObject, "PrayEP");
        mPolishEP = UITool.FindChild<Text>(gameObject, "PolishEP");

        mPrayBtn.onClick.AddListener(() => BusinessManager.Instance.Pray());
        mPolishBtn.onClick.AddListener(() => BusinessManager.Instance.Polish());
        mBusinessBtn.onClick.AddListener(() => BusinessManager.Instance.Business());
        UpdateEP();
    }

    public  void UpdateEP()
    {

        mPrayEP.text ="EP"+ BusinessManager.Instance.CalcPrayEP().ToString();
        mPolishEP.text ="EP"+ BusinessManager.Instance.CalcPolishEP() .ToString();
    }

    public override void Show()
    {
        base.Show();
        UpdateEP();
    }
}
