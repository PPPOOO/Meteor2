using UnityEngine;
using System.Collections;

public class ItemPet : OtherItem
{

    public int PetID;
    public ItemPet(int id, string name, string des, string sprite, int buyprice, int sellpirce, int capacity, ItemType type,
     OtherItemType othertype,
     int petID)
    : base(id, name, des, sprite, buyprice, sellpirce, capacity, type, othertype)
    {
        PetID = petID;
    }
}
