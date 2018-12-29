using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BagSlot : Slot
{
    public Item.ItemType itemType;

    public bool IsRightItem(Item item)
    {
        if (item.Type== this.itemType)
        {
            return true;
        }
        return false;
    }


    public override void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (PickedItem.Instance.IsPickedItem== false && transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                if (currentItemUI.Item.Type == Item.ItemType.Equipment)
                {
                    Item currentItem = currentItemUI.Item;
                    currentItemUI.ReduceAmount(1);
                    ToolTip.Instance.Hide();
                    PutOnPanel.Instance.PutOn(currentItem);
                }
                else if(currentItemUI.Item.Type == Item.ItemType.Consumable)
                {
                    Consumable item = currentItemUI.Item as Consumable;
                    for (int i = 0; i < item.ApplyAttrEffects.Count; i++)
                    {
                        AttrManager.Instance.ChangeAttrByType(ps.gameObject, item.ApplyAttrEffects[i]);
                    }
                    currentItemUI.ReduceAmount();
                }
            }
        }

        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (transform.childCount > 0)
        {
            ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();
            if (PickedItem.Instance.IsPickedItem==false)
            {
                PickedItem.Instance.SetPickItem(currentItem.Item.ID, currentItem.Amount);
                Destroy(currentItem.gameObject);//销毁当前物品
            }
            else
            {
                if(IsRightItem(PickedItem.Instance.item))//手上的物品类型和格子类型一样
                {
                    if (PickedItem.Instance.ID == currentItem.Item.ID)
                    {
                        if (currentItem.Item.Capacity > currentItem.Amount)
                        {
                            int amountRemain = currentItem.Item.Capacity - currentItem.Amount;//当前物品槽剩余的空间
                            if (amountRemain >= PickedItem.Instance.Count)
                            {
                                currentItem.SetItem(currentItem.Item, currentItem.Amount + PickedItem.Instance.Count);
                                PickedItem.Instance.Hide();
                            }
                            else
                            {
                                currentItem.SetItem(currentItem.Item, currentItem.Item.Capacity);
                                PickedItem.Instance.ReduceAmount(amountRemain);
                            }
                        }
                    }
                    else
                    {
                        Item tempitem = currentItem.Item;
                        int tempcount = currentItem.Amount;
                        currentItem.SetItem(PickedItem.Instance.item, PickedItem.Instance.Count);
                        PickedItem.Instance.SetPickItem(tempitem.ID, tempcount);
                    }
                }
            }
        }
        else
        {
            if (PickedItem.Instance.IsPickedItem && IsRightItem(PickedItem.Instance.item))
            {
                StoreItem(PickedItem.Instance.item, PickedItem.Instance.Count);
                PickedItem.Instance.Hide();
            }
        }
        
    }
    

}
