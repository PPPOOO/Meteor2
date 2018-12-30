using UnityEngine;
using System.Collections;

public class RanchHarvestChestPanel : BaseInventoryPanel
{
    private static RanchHarvestChestPanel _instance;
    public static RanchHarvestChestPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Canvas/RanchPanel/RanchHarvestChestPanel").GetComponent<RanchHarvestChestPanel>();
            }
            return _instance;
        }
    }

}
