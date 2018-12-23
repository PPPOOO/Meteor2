using UnityEngine;
using System.Collections;

public class Materials : Item
{

    public Materials(int id, string name, string des, string sprite, int buyprice, int sellpirce, int capacity, ItemType type
         )
        : base(id, name, des, sprite, buyprice, sellpirce, capacity, type)
    {
    }


}
