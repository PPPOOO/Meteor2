using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChoiceCropPanel : BaseInventoryPanel
{
    private static ChoiceCropPanel _instance;
    public static ChoiceCropPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Canvas/CropPanel/ChoiceCropPanel").GetComponent<ChoiceCropPanel>();
            }
            return _instance;
        }
    }


    public override void Show()
    {
        if (transform.Find("Scroll View/Viewport/Content").childCount != 0)
        {
            CropItemChoice[] cropItemChioces;
            cropItemChioces = transform.Find("Scroll View/Viewport/Content").GetComponentsInChildren<CropItemChoice>();
            foreach (CropItemChoice cropItemChioce in cropItemChioces)
            {
                cropItemChioce.DestroySelf();
            }
        }
        CropManager.Instance.CreatChoiceCrop();
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
        
    }
}
