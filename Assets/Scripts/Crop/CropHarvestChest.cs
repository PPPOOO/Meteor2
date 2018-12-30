using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropHarvestChest : MonoBehaviour {

    private void OnMouseDown()
    {
        if (MonoBehaviourTool.Instance.GetOverUI() == false) 
        HarvestChestPanel.Instance.Show();
    }
}
