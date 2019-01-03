using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Item Item { get; private set; }
    public int Amount { get; private set; }
    public int ID;

    public Image ItemImage;
    public Text AmountText;
    public GameObject PickedItem;

    public void SetItem(Item item, int amount = 1)
    {
        ID = item.ID;
        this.Item = item;
        this.Amount = amount;
        ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
        LitimCapacityShow();
    }

    public void AddAmount(int amount = 1)
    {
        this.Amount += amount;
        LitimCapacityShow();
    }

    public void ReduceAmount(int amount = 1)
    {
        this.Amount -= amount;
        LitimCapacityShow();
        if (transform.parent.parent.parent.name == "Bag_PutOnPanel")
        {
            InventoryManager.Instance.ItemChange(Item, -amount);
        }
        if (Amount <= 0)
        {
            Destroy(gameObject);
        }
    }


    //当前物品 跟 另一个物品 交换显示
    public void Exchange(ItemUI itemUI)
    {
        Item itemTemp = itemUI.Item;
        int amountTemp = itemUI.Amount;
        itemUI.SetItem(this.Item, this.Amount);
        this.SetItem(itemTemp, amountTemp);
    }



    public void LitimCapacityShow()
    {
        if (Item.Capacity > 1)
            AmountText.text = Amount.ToString();
        else
            AmountText.text = "";
    }
}
