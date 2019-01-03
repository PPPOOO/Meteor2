using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BaseInventoryPanel : MonoSingleton<BaseInventoryPanel>
{


    public Slot[] slotList;


    protected CanvasGroup canvasGroup;

    // Use this for initialization
    public virtual void Start()
    {
        slotList = GetComponentsInChildren<Slot>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (gameObject.transform.Find("CloseBtn")!= null)
        {
            Button closeBtn = UITool.GetButton(gameObject, "CloseBtn");
            closeBtn.onClick.AddListener(Hide);
        }
        Hide();
    }



    public bool StoreItem(int id)
    {
        Item item = InventoryManager.Instance.GetItemById(id);
        return StoreItem(item);
    }

    public bool StoreItem(Item item, int count = 1)
    {

        if (item == null)
        {
            Debug.LogWarning("要存储的物品的id不存在");
            return false;
        }
        if (item.Capacity == 1)
        {
            Slot slot = FindEmptySlot();
            if (slot == null)
            {
                Debug.LogWarning("没有空的物品槽");
                return false;
            }
            else
            {
                slot.StoreItem(item, count);//把物品存储到这个空的物品槽里面
            }
        }
        else
        {
            Slot slot = FindSameIdSlot(item);
            if (slot != null)
            {
                slot.StoreItem(item, count);
            }
            else
            {
                Slot emptySlot = FindEmptySlot();
                if (emptySlot != null)
                {
                    emptySlot.StoreItem(item, count);
                }
                else
                {
                    Debug.LogWarning("没有空的物品槽");
                    return false;
                }
            }
        }
        return true;
    }
    
    

    public bool ReduceItem(int id)
    {
        Item item = InventoryManager.Instance.GetItemById(id);
        return ReduceItem(item);
    }

    public bool ReduceItem(Item item, int count = 1)
    {
        if (item == null)
        {
            Debug.LogWarning("要删除的物品的id不存在");
            return false;
        }
        int i = 0;
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount > 0 && slot.GetItemId() == item.ID)
            {
                slot.transform.GetChild(0).GetComponent<ItemUI>().ReduceAmount();
                i++;
                if (i >= count)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public int CheckItemCount(Item item)
    {
        int itemCount = 0;
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount >= 1 && slot.GetItemId() == item.ID)
            {
                itemCount += slot.GetComponentInChildren<ItemUI>().Amount;
            }
        }
        return itemCount;
    }

    /// <summary>
    /// 这个方法用来找到一个空的物品槽
    /// </summary>
    /// <returns></returns>
    private Slot FindEmptySlot()
    {
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return null;
    }

    private Slot FindSameIdSlot(Item item)
    {
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount >= 1 && slot.GetItemId() == item.ID && slot.IsFilled() == false)
            {
                return slot;
            }
        }
        return null;
    }

    public virtual void Show()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        Tweener tweener = transform.DOMoveX(600, 0.3f);
        tweener.SetEase(Ease.InOutExpo);
        var top_idx = gameObject.transform.parent.childCount - 1;
        gameObject.transform.SetSiblingIndex(top_idx); // 放到顶层
    }
    public virtual void Hide()
    {
        Tweener tweener = transform.DOMoveX(2000, 0.3f);
        tweener.SetEase(Ease.InOutExpo);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }
    public void DisplaySwitch()
    {
        if (canvasGroup.alpha == 0)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
