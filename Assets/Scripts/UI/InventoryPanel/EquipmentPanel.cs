using UnityEngine;
using System.Collections;

public class EquipmentPanel : BaseInventoryPanel
{

    private static EquipmentPanel _instance;
    public static EquipmentPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Canvas/InventoryPanel/Bag_PutOnPanel/EquipmentPanel").GetComponent<EquipmentPanel>();
            }
            return _instance;
        }
    }
    public override void Start()
    {
        slotList = GetComponentsInChildren<Slot>();
        canvasGroup = GetComponent<CanvasGroup>();
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
