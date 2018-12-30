using UnityEngine;
using System.Collections;

public class BusinessStation : MonoBehaviour
{


    private void OnMouseDown()
    {
        if (MonoBehaviourTool.Instance.GetOverUI() == false)
            MSBusinessPanel.Instance.Show();
    }
}
