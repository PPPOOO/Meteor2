
using UnityEngine;
using System.Collections;

public class RanchAnimalInfo
{
    public int ID;
    public string Name;
    public string Animation;
    public int DayGrow;
    public int MaxGrow;
    public int ProductID;
    public RanchAnimalInfo(int id,string name,string animation,int daygrow,int maxgrow,int productid)
    {
        ID = id;
        Name = name;
        Animation = animation;
        DayGrow = daygrow;
        MaxGrow = maxgrow;
        ProductID = productid;
    }
}
