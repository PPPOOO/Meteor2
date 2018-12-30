using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSCropPanel : BasePanel<MSCropPanel>
{
    private Button mWaterBtn;
    private Button mFertilizeBtn;
    private Button mHarvestBtn;
    private Text mWaterEP;
    private Text mFertilizeEP;
    private Text mHarvestEP;


    public override void Start()
    {
        base.Start();
        mWaterBtn = UITool.GetButton(gameObject, "WaterBtn");
        mFertilizeBtn = UITool.GetButton(gameObject, "FertilizeBtn");
        mHarvestBtn = UITool.GetButton(gameObject, "HarvestBtn");
        mWaterEP = UITool.FindChild<Text>(gameObject, "WaterEP");
        mFertilizeEP = UITool.FindChild<Text>(gameObject, "FertilizeEP");
        mHarvestEP = UITool.FindChild<Text>(gameObject, "HarvestEP");
        
        mWaterBtn.onClick.AddListener(()=> CropManager.Instance.WaterCrop());
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
