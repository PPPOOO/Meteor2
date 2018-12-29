using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager _instance;

    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                //下面的代码只会执行一次
                _instance = GameObject.FindGameObjectWithTag("Manager").GetComponent<InventoryManager>();
            }
            return _instance;
        }
    }

    public bool IsQuestClear = true;
    public List<QuestItemUI> questItemUIs = new List<QuestItemUI>();
    public List<Item> itemList;
    protected  void Awake()
    {
        ParseItemJson();
    }

    void Start()
    {
        
    }



    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ChestPanel.Instance.DisplaySwitch();
        }
        if (Input.GetKey(KeyCode.G))
        {
            int randnum = Random.Range(0, itemList.Count);
            int id = itemList[randnum].ID;
            ChestPanel.Instance.StoreItem(id);
            
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Bag_PutOnPanel.Instance.DisplaySwitch();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            StatusPanel.Instance.DisplaySwitch();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuestPanel.Instance.DisplaySwitch();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SkillPanel.Instance.DisplaySwitch();
        }
    }


    public void GetItemQuest(QuestItemUI questItemUI)
    {
        IsQuestClear = false;
        questItemUIs.Add(questItemUI);
        switch (GetItemById(questItemUI.Quest.ItemID).Type)
        {
            case Item.ItemType.Consumable:
                FindQuestItem(ConsumablePanel.Instance.slotList);
                break;
            case Item.ItemType.Equipment:
                FindQuestItem(EquipmentPanel.Instance.slotList);
                break;
            case Item.ItemType.Materials:
                FindQuestItem(MaterialsPanel.Instance.slotList);
                break;
            case Item.ItemType.OtherItem:
                FindQuestItem(OtherItemPanel.Instance.slotList);
                break;
        }
    }

    public void FindQuestItem(Slot[] slotList )
    {
        foreach (QuestItemUI questItemUI in questItemUIs)
        {
            int Count = 0;
            foreach (Slot slot in slotList)
            {
                if (slot.transform.childCount > 0)
                {
                    if (slot.transform.GetComponentInChildren<ItemUI>().Item.ID == questItemUI.Quest.ItemID)
                    {
                        Count += slot.transform.GetComponentInChildren<ItemUI>().Amount;
                    }
                }
            }
            questItemUI.CurrentCount = Count;
        }
    }

    public void CheckItemIsQuest(Item item,int count=1)
    {
        foreach (QuestItemUI questItemUI in questItemUIs)
        {
            if(item.ID== questItemUI.Quest.ItemID)
            {
                questItemUI.CurrentCount += count;
            }
            questItemUI.UpdateShowDes(questItemUI.CurrentCount);
            
            if (questItemUI.CurrentCount >= questItemUI.Quest.Count)
            {
                foreach (NPCUI npc in NPCManager.Instance.QuestNPCList)
                {
                    if (questItemUI.Quest.NPCID == npc.ID)
                    {
                        npc.HideQuestIcon();
                        QuestManager.Instance.AddFinishQuestList(questItemUI);
                    }
                }
            }
        }
    }

    public Item GetItemById(int id)
    {

        foreach (Item item in itemList)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }


   public void ShiftCurrentItem(Item item,int count)
    {
        if (item.Type == Item.ItemType.Equipment)
        {
            EquipmentPanel.Instance.StoreItem(item, count);
            
        }
        else if (item.Type == Item.ItemType.Consumable)
        {
            ConsumablePanel.Instance.StoreItem(item, count);
        }
        else if (item.Type == Item.ItemType.Materials)
        {
            MaterialsPanel.Instance.StoreItem(item, count);
        }
        else if (item.Type == Item.ItemType.OtherItem)
        {
            OtherItemPanel.Instance.StoreItem(item, count);
        }
        else
        {
            Debug.LogError("没有对应类型的背包");
        }
    }

    

    void ParseItemJson()
    {
        itemList = new List<Item>();
        TextAsset itemText = Resources.Load<TextAsset>("ItemsInfo");
        string itemsJson = itemText.text;
        JSONObject j = new JSONObject(itemsJson);
        foreach(JSONObject temp in j.list)
        {
            Item item = null;
            int id = (int)temp["id"].n;
            string name = temp["name"].str;
            string des = temp["des"].str;
            string sprite = temp["sprite"].str;
            int buyprice =(int) temp["buyPrice"].n;
            int sellprice = (int)temp["sellPrice"].n;
            int capacity = (int)temp["capacity"].n;
            string strtype = temp["type"].str;
            Item.ItemType type = (Item.ItemType)System.Enum.Parse(typeof(Item.ItemType), strtype);
            switch (type)
            {
                case Item.ItemType.Consumable:
                    JSONObject j3 = temp["applyAttr"];
                    List<ApplyAttrEffect> applyAttrEffects2 = new List<ApplyAttrEffect>();
                    if (j3.type == JSONObject.Type.ARRAY)
                    {
                        foreach (JSONObject temp2 in j3.list)
                        {
                            ApplyAttrEffect applyAttrEffect = null;
                            AttrType attrType = (AttrType)System.Enum.Parse(typeof(AttrType), temp2["attrTpye"].str);
                            bool positive = temp2[1].b;
                            int value = (int)temp2[2].n;
                            applyAttrEffect = new ApplyAttrEffect(attrType, positive, value);
                            applyAttrEffects2.Add(applyAttrEffect);
                        }
                    }
                    else if (j3.type == JSONObject.Type.OBJECT)
                    {
                        ApplyAttrEffect applyAttrEffect = null;
                        AttrType attrType = (AttrType)System.Enum.Parse(typeof(AttrType), j3["attrTpye"].str);
                        bool positive = j3[1].b;
                        int value = (int)j3[2].n;
                        applyAttrEffect = new ApplyAttrEffect(attrType, positive, value);
                        applyAttrEffects2.Add(applyAttrEffect);
                    }
                    item = new Consumable(id, name, des, sprite, buyprice, sellprice, capacity, type, applyAttrEffects2);
                    break;
                case Item.ItemType.Equipment:
                    string stret = temp["equipType"].str;
                    Equipment.EquipType equipType = (Equipment.EquipType)System.Enum.Parse(typeof(Equipment.EquipType), stret);
                    JSONObject j2 = temp["applyAttr"];
                    List<ApplyAttrEffect> applyAttrEffects = new List<ApplyAttrEffect>();
                    if (j2.type == JSONObject.Type.ARRAY)
                    {
                        foreach(JSONObject temp2 in j2.list)
                        {
                            ApplyAttrEffect applyAttrEffect = null;
                            AttrType attrType = (AttrType)System.Enum.Parse(typeof(AttrType), temp2["attrTpye"].str);
                            bool positive = temp2[1].b;
                            int value = (int)temp2[2].n;
                            applyAttrEffect = new ApplyAttrEffect(attrType, positive, value);
                            applyAttrEffects.Add(applyAttrEffect);
                        }
                    }
                    else if(j2.type == JSONObject.Type.OBJECT)
                    {
                        ApplyAttrEffect applyAttrEffect = null;
                        AttrType attrType = (AttrType)System.Enum.Parse(typeof(AttrType), j2["attrTpye"].str);
                        bool positive = j2[1].b;
                        int value = (int)j2[2].n;
                        applyAttrEffect = new ApplyAttrEffect(attrType, positive, value);
                        applyAttrEffects.Add(applyAttrEffect);
                    }
                    item = new Equipment(id, name, des, sprite, buyprice, sellprice, capacity, type, equipType, applyAttrEffects);
                    break;
                case Item.ItemType.Materials:
                    item = new Materials(id, name, des, sprite, buyprice, sellprice, capacity, type);
                    break;
                case Item.ItemType.OtherItem:
                    OtherItem.OtherItemType otherItemType = (OtherItem.OtherItemType)System.Enum.Parse(typeof(OtherItem.OtherItemType), temp["othertype"].str);
                    switch (otherItemType)
                    {
                        case OtherItem.OtherItemType.Seed:
                            int maxgrow =(int) temp["maxgrow"].n;
                            int daygrow = (int)temp["daygrow"].n;
                            int productid = (int)temp["productid"].n;
                            JSONObject j4 = temp["diffsprites"];
                            List<string> diffsprites = new List<string>();
                            for(int i = 0; i < j4.list.Count; i++)
                            {
                                string diffsprite = j4[i].str;
                                diffsprites.Add(diffsprite);
                            }
                            item = new ItemSeed(id, name, des, sprite, buyprice, sellprice, capacity, type, otherItemType, maxgrow, daygrow, productid, diffsprites);
                            break;
                        case OtherItem.OtherItemType.Pet:
                            int petid = (int)temp["petid"].n;
                            item = new ItemPet(id, name, des, sprite, buyprice, sellprice, capacity, type,otherItemType, petid);
                            break;
                    }
                    break;
            }
            itemList.Add(item);
        }
    }
}