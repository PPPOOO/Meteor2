using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OtherItemPanel : BaseInventoryPanel
{

    private static OtherItemPanel _instance;
    public static OtherItemPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Canvas/InventoryPanel/Bag_PutOnPanel/OtherItemPanel").GetComponent<OtherItemPanel>();
            }
            return _instance;
        }
    }
    public override void Show()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1; var top_idx = gameObject.transform.parent.childCount - 1;
        gameObject.transform.SetSiblingIndex(top_idx);
    }
    public override void Hide()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }

    public List<ItemUI> FindSeed()
    {
        List<ItemUI> seeditemUIs = new List<ItemUI>();
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                OtherItem item = slot.transform.GetChild(0).GetComponent<ItemUI>().Item as OtherItem;
                if (item.OtherType == OtherItem.OtherItemType.Seed)
                {
                    ItemUI seedItemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                    seeditemUIs.Add(seedItemUI);
                }
            }
        }
        return seeditemUIs;
    }

    public List<ItemUI> FindRanchAnimal()
    {
        List<ItemUI> RanchAnimalList = new List<ItemUI>();
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                OtherItem item = slot.transform.GetChild(0).GetComponent<ItemUI>().Item as OtherItem;
                if (item.OtherType == OtherItem.OtherItemType.Pet)
                {
                    ItemUI RanchAnimal = slot.transform.GetChild(0).GetComponent<ItemUI>();
                    RanchAnimalList.Add(RanchAnimal);
                }
            }
        }
        return RanchAnimalList;
    }
}
