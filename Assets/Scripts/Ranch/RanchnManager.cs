using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RanchnManager : MonoSingleton<RanchnManager>
{
    private List<RanchAnimalInfo> RanchAnimalInfoList = new List<RanchAnimalInfo>();

    public List<RanchAnimalUI> RanchAnimalUIList = new List<RanchAnimalUI>();
    public List<RanchAnimalUI> RanchAnimalUIGrowList = new List<RanchAnimalUI>();
    private GameObject RanchAnimalChoiceItemPrefab;
    private GameObject RanchAnimalItemPrefab;
    private Transform mContent;
    public Transform RanchPos;
    private GameObject player;


    public bool isPacify;
    public bool isFeed;

    public int pacifyEP;
    public int feedEP;
    public int harvestEP;

    protected override void Awake()
    {
        base.Awake();
        ParseRanchAnimalInfo();
    }


    private void Start()
    {
        RanchAnimalItemPrefab= Resources.Load<GameObject>("Ranch/RanchAnimalItem");
        RanchAnimalChoiceItemPrefab = Resources.Load<GameObject>("Ranch/AnimalItemChioce");
        mContent = GameObject.FindGameObjectWithTag("Canvas").transform.Find("RanchPanel/ChoiceRanchAnimalPanel/Scroll View/Viewport/Content");
        player = GameObject.FindGameObjectWithTag("Player");
        GameEventManager.Instance.RegisterObserver(GameEventType.NewDay, new NewDayObserverRanch(this));
    }

    public RanchAnimalInfo GetRanchAnimalInfoByID(int id)
    {
        foreach(RanchAnimalInfo ranchAnimalInfo in RanchAnimalInfoList)
        {
            if (ranchAnimalInfo.ID == id)
            {
                return ranchAnimalInfo;
            }
        }
        return null;
    }

    public int GetPacifyEP()
    {
        return pacifyEP = RanchAnimalUIList.Count * 5;
    }
    public int GetFeedEP()
    {
        return feedEP = RanchAnimalUIList.Count * 5;
    }
    public int GetHarvestEP()
    {
        return harvestEP = RanchAnimalUIGrowList.Count * 10;
    }

    public void AllPacify()
    {
        if (!isPacify)
        {
            if (player.GetComponent<PlayerStatus>().TakeEP(GetPacifyEP()))
            {
                for (int i = 0; i <= RanchAnimalUIList.Count - 1; i++)
                {
                    RanchAnimalUIList[i].Pacify();
                }

                isPacify = true;
                ToolTip.Instance.ShowForTimeInMousePosition("安抚成功！！", 2);
                MSRanchPanel.Instance.Hide();
            }
            else
            {
                ToolTip.Instance.ShowForTimeInMousePosition("体力不够！！", 2);
                ToolTip.Instance.transform.position = Input.mousePosition;
            }
        }
        else
        {
            ToolTip.Instance.ShowForTimeInMousePosition("今天安抚过啦！", 2);
        }
    }

    public void AllFeed()
    {
        if (!isFeed)
        {
            if (player.GetComponent<PlayerStatus>().TakeEP(GetFeedEP()))
            {
                for (int i = 0; i <= RanchAnimalUIList.Count - 1; i++)
                {
                    RanchAnimalUIList[i].Feed();
                }

                isFeed = true;
                ToolTip.Instance.ShowForTimeInMousePosition("喂养成功！！", 2);
                MSRanchPanel.Instance.Hide();
            }
            else
            {
                ToolTip.Instance.ShowForTimeInMousePosition("体力不够！！", 2);
                ToolTip.Instance.transform.position = Input.mousePosition;
            }
        }
        else
        {
            ToolTip.Instance.ShowForTimeInMousePosition("今天喂养过啦！", 2);
        }
    }

    public void Allharvest()
    {
        if (player.GetComponent<PlayerStatus>().TakeEP(GetHarvestEP()))
        {
            if (RanchAnimalUIGrowList.Count == 0)
            {
                ToolTip.Instance.ShowForTimeInMousePosition("你根本没有可以收获的日产！！", 2);
                return;
            }
            for (int i = RanchAnimalUIList.Count - 1; i >= 0; i--)
            {
                RanchAnimalUIList[i].Harvest();
            }
            ToolTip.Instance.ShowForTimeInMousePosition("收获成功", 2);
            MSRanchPanel.Instance.Hide();
        }
        else
        {
            ToolTip.Instance.ShowForTimeInMousePosition("体力不够！！", 2);
        }
    }


    public void UpdateRanch()
    {
        for (int i = 0; i <= RanchAnimalUIList.Count - 1; i++)
        {
            RanchAnimalUIList[i].Grow();
        }
    }
    public void NewDayCome()
    {
        isPacify = false;
        isFeed = false;
        UpdateRanch();
    }


    public void CreatChoiceRanchAnimal()
    {
        List<ItemUI> itemUIs = new List<ItemUI>();
        itemUIs = OtherItemPanel.Instance.FindRanchAnimal();
        if (itemUIs == null) return;
        foreach (ItemUI itemUI in itemUIs)
        {
            GameObject RanchAnimalChoiceItem = Instantiate(RanchAnimalChoiceItemPrefab, mContent, false);
            RanchAnimalChoiceItem.GetComponent<RanchAnimalItemChoice>().SetAnimal(itemUI.Item.ID);
        }
    }

    public void CreatAnimalInRanch(int id)
    {
        GameObject RanchAnimalItem = Instantiate(RanchAnimalItemPrefab, RanchPos,false);
        RanchAnimalItem.transform.localPosition = Vector3.zero;
        RanchAnimalItem.GetComponent<RanchAnimalUI>().SetID(id);
    }

    private void ParseRanchAnimalInfo()
    {
        TextAsset ranchAnimalText = Resources.Load<TextAsset>("Json/RanchAnimalInfo");
        string ranchAnimalJson = ranchAnimalText.text;
        JSONObject j = new JSONObject(ranchAnimalJson);
        foreach(JSONObject temp in j.list)
        {
            int id = (int)temp["id"].n;
            string name = temp["name"].str;
            string animation = temp["animation"].str;
            int daygrow = (int)temp["daygrow"].n;
            int maxgrow = (int)temp["maxgrow"].n;
            int productid = (int)temp["productid"].n;
            RanchAnimalInfo ranchAnimalInfo = new RanchAnimalInfo(id, name, animation, daygrow, maxgrow, productid);
            RanchAnimalInfoList.Add(ranchAnimalInfo);
        }
    }
}
