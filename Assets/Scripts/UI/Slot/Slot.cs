using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    protected GameObject itemPrefab;
    protected PlayerStatus ps;
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        itemPrefab = Resources.Load<GameObject>("Item");
    }


    public void StoreItem(Item item, int count = 1)
    {
        if (transform.childCount == 0)
        {
            GameObject itemGameObject = Instantiate(itemPrefab);
            itemGameObject.transform.SetParent(this.transform);
            itemGameObject.transform.localPosition = Vector3.zero;
            itemGameObject.GetComponent<ItemUI>().SetItem(item, count);
        }
        else
        {
            transform.GetChild(0).GetComponent<ItemUI>().AddAmount(count);
        }
        if(transform.parent.parent.name== "Bag_PutOnPanel")
        {
            InventoryManager.Instance.ItemChange(item, count);
        }
    }

    #region 得到当前格子里的物品类型，物品id，和是否达到数量上限。主要用于储存新物品时对背包内格子物品的检测
    public Item.ItemType GetItemType()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.Type;
    }

    public int GetItemId()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.ID;
    }

    public bool IsFilled()
    {
        ItemUI itemUI = transform.GetChild(0).GetComponent<ItemUI>();
        return itemUI.Amount >= itemUI.Item.Capacity;//当前的数量大于等于容量
    }
    #endregion
    //隐藏tooltip
    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTip.Instance.Hide();
    }
    //显示tooltip
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.childCount > 0&&PickedItem.Instance.IsPickedItem==false)
        {
            string toolTipText = transform.GetChild(0).GetComponent<ItemUI>().Item.GetToolTipText();
            ToolTip.Instance.ShowFollowMouseWithOffset(toolTipText);
        }

    }

    //手上物品与格子的各种交互情况
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right) //右键当前格子 
        {
            if (PickedItem.Instance.IsPickedItem == false && transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                if (currentItemUI.Item is Equipment)
                {
                    Item currentItem = currentItemUI.Item;
                    currentItemUI.ReduceAmount(1);
                    ToolTip.Instance.Hide();
                    PutOnPanel.Instance.PutOn(currentItem);
                }
            }
        }

        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (transform.childCount > 0)//左键当前格子 当前格子有物品
        {
            ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();

            if (PickedItem.Instance.IsPickedItem==false)//当前手上没有任何物品
            {
                if (Input.GetKey(KeyCode.LeftShift))//按住左shift键快速移动物品
                {
                    InventoryManager.Instance.ShiftCurrentItem(currentItem.Item, currentItem.Amount);
                    Destroy(currentItem.gameObject);
                    ToolTip.Instance.Hide();
                }
                else//把物品拿起来
                {
                    PickedItem.Instance.SetPickItem(currentItem.Item.ID, currentItem.Amount);
                    Destroy(currentItem.gameObject);//销毁当前物品
                }
            }
            else//手上有物品 格子有物品
            {     
                if (currentItem.Item.ID == PickedItem.Instance.ID)//手上的物品id和格子的物品id一样
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
                else//手上东西和格子里东西不一样 交互id和数量
                {
                    Item tempitem = currentItem.Item;
                    int tempcount = currentItem.Amount;
                    currentItem.SetItem(PickedItem.Instance.item, PickedItem.Instance.Count);
                    PickedItem.Instance.SetPickItem(tempitem.ID, tempcount);
                    
                }

            }
        }
        else//当前格子里没有东西
        {
            if (PickedItem.Instance.IsPickedItem == true)
            {
                StoreItem(PickedItem.Instance.item, PickedItem.Instance.Count);
                PickedItem.Instance.Hide();
            }
        }
    }


}
