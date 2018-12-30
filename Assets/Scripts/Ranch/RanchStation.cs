using UnityEngine;
using System.Collections;

public class RanchStation : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (MonoBehaviourTool.Instance.GetOverUI() == false)
            MSRanchPanel.Instance.Show();
    }
}
