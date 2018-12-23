using UnityEngine;
using System.Collections;


public class Item
{

    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Sprite { get; set; }
    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }
    public int Capacity { get; set; }
    public ItemType Type { get; set; }


    public Item(int id, string name,string des,string sprite,int buyprice,int sellprice,int capacity,ItemType type)
    {
        this.ID = id;
        this.Name = name;
        this.Type = type;
        this.Description = des;
        this.Capacity = capacity;
        this.BuyPrice = buyprice;
        this.SellPrice = sellprice;
        this.Sprite = sprite;
    }



    /// <summary>
    /// 物品类型
    /// </summary>
    public enum ItemType
    {
        Consumable,
        Equipment,
        Materials,
        OtherItem
    }

    /// <summary> 
    /// 得到提示面板应该显示什么样的内容
    /// </summary>
    /// <returns></returns>
    public virtual string GetToolTipText()
    {
        string text = string.Format("<size=25>{0}</size>\n{1}\n", Name, Description);
        return text;
    }
}