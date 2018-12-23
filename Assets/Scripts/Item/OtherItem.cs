using UnityEngine;
using System.Collections;

public class OtherItem : Item
{
    public enum OtherItemType
    {
        None,
        Seed,
        Pet
    }


    public OtherItemType OtherType;
    public OtherItem(int id, string name, string des, string sprite, int buyprice, int sellpirce, int capacity, ItemType type,
         OtherItemType othertype)
        : base(id, name, des, sprite, buyprice, sellpirce, capacity, type)
    {
        OtherType = othertype;
    }

}
