using UnityEngine;
using System.Collections;

public class PutOnPanel : BaseInventoryPanel
{
    private static PutOnPanel _instance;
    public static PutOnPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Canvas/InventoryPanel/Bag_PutOnPanel/PutOnPanel").GetComponent<PutOnPanel>();
            }
            return _instance;
        }
    }


    public int Str;
    public int Agi;
    public int Mag;
    public int Vit;
    public int Damage;
    

    public override void Start()
    {
        slotList = GetComponentsInChildren<Slot>();
        canvasGroup = GetComponent<CanvasGroup>();
    }


    public void PutOn(Item item)
    {
        Item exitItem = null;
        foreach (Slot slot in slotList)
        {
            PutOnSlot equipmentSlot = (PutOnSlot)slot;
            if (equipmentSlot.IsRightItem(item))
            {
                if (equipmentSlot.transform.childCount > 0)
                {
                    ItemUI currentItemUI = equipmentSlot.transform.GetChild(0).GetComponent<ItemUI>();
                    exitItem = currentItemUI.Item;
                    currentItemUI.SetItem(item, 1);
                }
                else
                {
                    equipmentSlot.StoreItem(item);
                }
                break;
            }
        }
        if (exitItem != null)
        {
            EquipmentPanel.Instance.StoreItem(exitItem);
        }
        UpdateProperty();
    }

    public void PutOff(Item item)
    {
        EquipmentPanel.Instance.StoreItem(item);
        UpdateProperty();
    }

    public void UpdateProperty()
    {
        Str = 0;
        Agi = 0;
        Mag = 0;
        Vit = 0;
        Damage = 0;
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                Item item = slot.transform.GetChild(0).GetComponent<ItemUI>().Item;
                if (item is Equipment)
                {
                    Equipment e = (Equipment)item;
                    foreach(ApplyAttrEffect applyAttrEffect in e.ApplyAttrEffects)
                    {
                        if (applyAttrEffect.AT == AttrType.AD) Str +=(int) applyAttrEffect.FixValue;
                        if (applyAttrEffect.AT == AttrType.AGI) Agi += (int)applyAttrEffect.FixValue;
                        if (applyAttrEffect.AT == AttrType.MAG) Mag += (int)applyAttrEffect.FixValue;
                        if (applyAttrEffect.AT == AttrType.VIT) Vit += (int)applyAttrEffect.FixValue;
                        if (applyAttrEffect.AT == AttrType.AD) Damage += (int)applyAttrEffect.FixValue;
                    }
                }
            }
        }
        StatusPanel.Instance.UpdateStatusPanel();
    }


}

