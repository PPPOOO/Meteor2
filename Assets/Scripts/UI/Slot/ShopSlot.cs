using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ShopSlot : Slot
{
    

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (transform.childCount>0&&PickedItem.Instance.IsPickedItem == false)
            {
                Item currentitem = transform.GetChild(0).GetComponent<ItemUI>().Item;
                if (ps.TakeCoin(currentitem.BuyPrice))
                {
                    switch (currentitem.Type)
                    {
                        case Item.ItemType.Consumable:
                            ConsumablePanel.Instance.StoreItem(currentitem);
                            break;
                        case Item.ItemType.Equipment:
                            EquipmentPanel.Instance.StoreItem(currentitem);
                            break;
                        case Item.ItemType.Materials:
                            MaterialsPanel.Instance.StoreItem(currentitem);
                            break;
                        case Item.ItemType.OtherItem:
                            OtherItemPanel.Instance.StoreItem(currentitem);
                            break;
                    }
                    transform.GetChild(0).GetComponent<ItemUI>().ReduceAmount();
                    transform.parent.parent.SendMessage("UpdateCoin");
                }
                else
                {
                    ToolTip.Instance.ShowFollowMouse("金钱不够！！");
                }
            }
        }
        else if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (PickedItem.Instance.IsPickedItem == true)
            {
                ConfirmPanel.Instance.Show();
                StartCoroutine(SellItemConfirm(PickedItem.Instance.item, PickedItem.Instance.Count));

            }
            else
            {
                if (transform.childCount > 0)
                {
                    if (ps.CoinCount>= transform.GetChild(0).GetComponent<ItemUI>().Item.SellPrice)
                    {
                        ConfirmPanel.Instance.Show();
                        StartCoroutine(BuyItemConfirm(transform.GetChild(0).GetComponent<ItemUI>().Item));
                    }
                    else
                    {
                        ToolTip.Instance.ShowFollowMouse("金钱不够！！");
                    }
                }
            }
        }
    }


    IEnumerator SellItemConfirm(Item item,int count)
    {
        while (1 > 0)
        {
            yield return new WaitForSeconds(0.05f);
            if (ConfirmPanel.Instance.IsClickOK)
            {
                ps.CoinUP(item.SellPrice*count);
                if (InventoryManager.Instance.IsQuestClear == false)
                {
                    InventoryManager.Instance.CheckItemIsQuest(item, -count);
                }
                PickedItem.Instance.Hide();
                break;
            }
            if (ConfirmPanel.Instance.IsClickCancel)
            {
                break;
            }
        }
    }

    IEnumerator BuyItemConfirm(Item item)
    {
        while (1 > 0)
        {
            yield return new WaitForSeconds(0.05f);
            if (ConfirmPanel.Instance.IsClickOK)
            {
                ps.CoinDown(item.BuyPrice);
                switch (item.Type)
                {
                    case Item.ItemType.Consumable:
                        ConsumablePanel.Instance.StoreItem(item);

                        break;
                    case Item.ItemType.Equipment:
                        EquipmentPanel.Instance.StoreItem(item);
                        
                        break;
                    case Item.ItemType.Materials:
                        MaterialsPanel.Instance.StoreItem(item);
                        break;
                    case Item.ItemType.OtherItem:
                        OtherItemPanel.Instance.StoreItem(item);
                        break;
                }
                transform.GetChild(0).GetComponent<ItemUI>().ReduceAmount();
                transform.parent.parent.SendMessage("UpdateCoin");
                yield return new WaitForSeconds(0.05f);
                break;
            }
            if (ConfirmPanel.Instance.IsClickCancel)
            {
                break;
            }
        }
    }




}
