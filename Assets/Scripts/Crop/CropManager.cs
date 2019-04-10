using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CropManager: MonoBehaviour
{

    private static CropManager _instance;
    public static CropManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag("Manager").GetComponent<CropManager>();
            }
            return _instance;
        }
    }

    private List<CropItem> crops = new List<CropItem>();
    private List<CropItem> growCrops = new List<CropItem>();
    private GameObject player;
    private BoxCollider2D col;
    public GameObject CropChoiceItem;
    public bool IsWater=false;
    public bool IsFertilize = false;
    public int WaterEP=0;
    public int FertilizeEP=0;
    public int HarvestEP=0;
    private List<ItemUI> seedItemUIs;
    public Transform Croplandtransform;
    private Transform mContent;

    private void Start()
    {
        CropChoiceItem = Resources.Load<GameObject>("Crop/CropItemChoice");
        mContent = GameObject.FindGameObjectWithTag("Canvas").transform.Find("CropPanel/ChoiceCropPanel/Scroll View/Viewport/Content");
        player = GameObject.FindGameObjectWithTag("Player");
        col = player.GetComponent<BoxCollider2D>();
        GameEventManager.Instance.RegisterObserver(GameEventType.NewDay, new NewDayObserverCropManager(this));
    }



    private void Update()
    {
        CheckDistance();
    }


    public void CalcWaterEP()
    {
        WaterEP = crops.Count * 5;
    }

    public void CalcFertilizeEP()
    {
        FertilizeEP = crops.Count * 10;
    }

    public void CalcHarvestEP()
    {
        HarvestEP = growCrops.Count * 15;
    }

    public void WaterCrop()
    {
        if (IsWater == false)
        {
            if (player.GetComponent<PlayerStatus>().TakeEP(WaterEP))
            {
                for(int i = 0; i <= crops.Count - 1; i++)
                {
                    crops[i].Water(1);
                }
                
                IsWater = true;
                ToolTip.Instance.ShowForTimeInMousePosition("浇水成功！！", 2);
                MSCropPanel.Instance.Hide();
            }
            else
            {
                ToolTip.Instance.ShowForTimeInMousePosition("体力不够！！",2);
            }
        }
        else
        {
            ToolTip.Instance.ShowForTimeInMousePosition("今天浇过水啦！", 2);
        }
    }

    public void FertilizeCrop()
    {
        if (IsFertilize == false)
        {
            if (IsWater)
            {
                if (player.GetComponent<PlayerStatus>().TakeEP(FertilizeEP))
                {
                    for (int i = 0; i <= crops.Count - 1; i++)
                    {
                        crops[i].Fertilize(2);
                    }
                    IsFertilize = true;
                    ToolTip.Instance.ShowForTimeInMousePosition("施肥成功！！", 2);
                    MSCropPanel.Instance.Hide();
                }
                else
                {
                    ToolTip.Instance.ShowForTimeInMousePosition("体力不够！！", 2);
                }
            }
            else
            {
                ToolTip.Instance.ShowForTimeInMousePosition("必须先浇水才能施肥！", 2);
            }
        }
        else
        {
            ToolTip.Instance.ShowForTimeInMousePosition("今天施过肥啦！", 2);
        }
    }

    public void HarvestCrop()
    {
        if (player.GetComponent<PlayerStatus>().TakeEP(HarvestEP))
        {
            if (growCrops.Count == 0)
            {
                ToolTip.Instance.ShowForTimeInMousePosition("你根本没有可以收获的作物！！", 2);
                return;
            }
            for (int i = growCrops.Count - 1; i >=0 ; i--)
            {
                growCrops[i].Harvest(1);
            }
            ToolTip.Instance.ShowForTimeInMousePosition("收获成功", 2);
            MSCropPanel.Instance.Hide();
        }
        else
        {
            ToolTip.Instance.ShowForTimeInMousePosition("体力不够！！", 2);
        }
        
    }

    public void CheckDistance()
    {
        if (Croplandtransform == null) return;
        float distance = Vector3.Distance(player.transform.position, Croplandtransform.position);
        if (distance > 5)
        {
            ChoiceCropPanel.Instance.Hide();
        }
    }


    public void UpdateCrop()
    {
        for (int i = 0; i <= crops.Count - 1; i++)
        {
            crops[i].Grow();
        }
    }



    public void AddCrop(CropItem crop)
    {
        crops.Add(crop);
        CalcWaterEP();
        CalcFertilizeEP();
    }
    public void RemoveCrop(CropItem crop)
    {
        crops.Remove(crop);
        CalcWaterEP();
        CalcFertilizeEP();
    }

    public void AddGrowCrop(CropItem growcrop)
    {
        growCrops.Add(growcrop);
        CalcHarvestEP();
    }
    public void RemoveGrowCrop(CropItem growcrop)
    {
        growCrops.Remove(growcrop);
        CalcHarvestEP();
    }


    public void NewDayCome()
    {
        IsFertilize = false;
        IsWater = false;
        UpdateCrop();
    }


    public void CreatChoiceCrop()
    {
        seedItemUIs = new List<ItemUI>();
        seedItemUIs = OtherItemPanel.Instance.FindSeed();
        if (seedItemUIs == null) return;
        foreach(ItemUI itemUI in seedItemUIs)
        {
            GameObject SeedItem = Instantiate(CropChoiceItem, mContent, false);
            SeedItem.GetComponent<CropItemChoice>().SetSeed(itemUI.Item.ID,itemUI.Amount);
        }
    }


}
