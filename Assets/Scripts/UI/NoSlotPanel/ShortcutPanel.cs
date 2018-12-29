using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutPanel : BaseInventoryPanel
{

    private static ShortcutPanel _instance;
    public static ShortcutPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag("Canvas").transform.Find("ShortcutPanel").GetComponent<ShortcutPanel>();
            }
            return _instance;
        }
    }

    public override void Start()
    {
        slotList = GetComponentsInChildren<Slot>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
}
