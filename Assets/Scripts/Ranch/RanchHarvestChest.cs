using UnityEngine;
using System.Collections;

public class RanchHarvestChest : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (MonoBehaviourTool.Instance.GetOverUI() == false)
            RanchHarvestChestPanel.Instance.Show();
    }
}
