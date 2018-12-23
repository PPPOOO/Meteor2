using UnityEngine;
using System.Collections;

public class ChestPanel : BaseInventoryPanel
{
    private static ChestPanel _instance;
    public static ChestPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag("Canvas").transform.Find("InventoryPanel/ChestPanel").GetComponent<ChestPanel>();
            }
            return _instance;
        }
    }


}
