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

    public void AllPacify()
    {

    }

    public void AllFeed()
    {

    }

    public void Allharvest()
    {

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
        GameObject RanchAnimalItem = Instantiate(RanchAnimalItemPrefab, RanchPos.position,Quaternion.identity);
        RanchAnimalItem.GetComponent<RanchAnimalUI>().SetID( id);
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
