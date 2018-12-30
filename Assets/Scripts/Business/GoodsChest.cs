using UnityEngine;
using System.Collections;

public class GoodsChest : MonoBehaviour
{

    private void OnMouseDown()
    {
        if (MonoBehaviourTool.Instance.GetOverUI() == false)
        {
            GoodsChestPanel.Instance.Show();
            Bag_PutOnPanel.Instance.Show();
        }
    }

    
}
