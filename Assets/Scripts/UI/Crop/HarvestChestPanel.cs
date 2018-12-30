using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarvestChestPanel : BaseInventoryPanel
{
    private static HarvestChestPanel _instance;
    public static HarvestChestPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag("Canvas").transform. Find("CropPanel/HarvestChestPanel").GetComponent<HarvestChestPanel>();
            }
            return _instance;
        }
    }

}
