using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemSeed : OtherItem
{

    public int Maxgrow;
    public int Daygrow;
    public int ProductID;
    public List<string> Sprites;

    public ItemSeed(int id, string name, string des, string sprite, int buyprice, int sellpirce, int capacity, ItemType type,
     OtherItemType othertype,
     int maxgrow,int daygrow,int productid, List<string> sprites)
    : base(id, name, des, sprite, buyprice, sellpirce, capacity, type, othertype)
    {
        Maxgrow = maxgrow;
        Daygrow = daygrow;
        ProductID = productid;
        Sprites = sprites;
    }
}
