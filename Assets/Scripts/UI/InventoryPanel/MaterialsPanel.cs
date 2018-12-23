using UnityEngine;
using System.Collections;

public class MaterialsPanel : BaseInventoryPanel
{

    private static MaterialsPanel _instance;
    public static MaterialsPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Canvas/InventoryPanel/Bag_PutOnPanel/MaterialsPanel").GetComponent<MaterialsPanel>();
            }
            return _instance;
        }
    }
    public override void Show()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1; var top_idx = gameObject.transform.parent.childCount - 1;
        gameObject.transform.SetSiblingIndex(top_idx);
    }
    public override void Hide()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }
}
