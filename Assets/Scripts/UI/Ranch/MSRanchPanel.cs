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

        mWaterBtn.onClick.AddListener(() => CropManager.Instance.WaterCrop());
        mFertilizeBtn.onClick.AddListener(() => CropManager.Instance.FertilizeCrop());
        mHarvestBtn.onClick.AddListener(() => CropManager.Instance.HarvestCrop());
        UpdateEP();
    }

    void UpdateEP()
    {
        mWaterEP.text = CropManager.Instance.WaterEP.ToString();
        mFertilizeEP.text = CropManager.Instance.FertilizeEP.ToString();
        mHarvestEP.text = CropManager.Instance.HarvestEP.ToString();
    }

    public override void Show()
    {
        base.Show();
        UpdateEP();
    }

}
