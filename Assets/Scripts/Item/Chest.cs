using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (MonoBehaviourTool.Instance.GetOverUI() == false)
            ChestPanel.Instance.Show();
    }
}
