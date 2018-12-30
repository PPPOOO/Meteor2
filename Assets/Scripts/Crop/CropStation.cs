using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropStation : MonoBehaviour {

    private void OnMouseDown()
    {
        if (MonoBehaviourTool.Instance.GetOverUI() == false)
            MSCropPanel.Instance.Show();
    }
}
