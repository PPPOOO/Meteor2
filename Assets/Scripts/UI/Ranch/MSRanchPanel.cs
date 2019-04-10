using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MSRanchPanel : BasePanel<MSRanchPanel>
{

    private Button mWaterBtn;
    private Button mFertilizeBtn;
    private Button mHarvestBtn;
    private Text mWaterEP;
    private Text mFertilizeEP;
    private Text mHarvestEP;
    private Button mChoiceAnimalBtn;

    public override void Start()
    {
        base.Start();
        mWaterBtn = UITool.GetButton(gameObject, "WaterBtn");
        mFertilizeBtn = UITool.GetButton(gameObject, "FertilizeBtn");
        mHarvestBtn = UITool.GetButton(gameObject, "HarvestBtn");
        mWaterEP = UITool.FindChild<Text>(gameObject, "WaterEP");
        mFertilizeEP = UITool.FindChild<Text>(gameObject, "FertilizeEP");
        mHarvestEP = UITool.FindChild<Text>(gameObject, "HarvestEP");

        mChoiceAnimalBtn = UITool.GetButton(gameObject, "ChoiceAnimalBtn");
        mChoiceAnimalBtn.onClick.AddListener(()=> {
            ChoiceRanchAnimalPanel.Instance.Show();
            Hide();
            });

        mWaterBtn.onClick.AddListener(() => RanchnManager.Instance.AllPacify());
        mFertilizeBtn.onClick.AddListener(() => RanchnManager.Instance.AllFeed());
        mHarvestBtn.onClick.AddListener(() => RanchnManager.Instance.Allharvest());
        UpdateEP();
    }

    void UpdateEP()
    {
        mWaterEP.text = RanchnManager.Instance.GetPacifyEP().ToString();
        mFertilizeEP.text = RanchnManager.Instance.GetFeedEP().ToString();
        mHarvestEP.text = RanchnManager.Instance.GetHarvestEP().ToString();
    }

    public override void Show()
    {
        base.Show();
        UpdateEP();
    }

}
