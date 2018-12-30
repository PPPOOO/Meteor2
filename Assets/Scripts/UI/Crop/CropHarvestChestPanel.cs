using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CropHarvestChestPanel : BaseInventoryPanel
{
    private static CropHarvestChestPanel _instance;
    public static CropHarvestChestPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag("Canvas").transform. Find("CropPanel/CropHarvestChestPanel").GetComponent<CropHarvestChestPanel>();
            }
            return _instance;
        }
    }

}
