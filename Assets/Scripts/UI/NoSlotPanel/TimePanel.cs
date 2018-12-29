using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePanel :BasePanel<TimePanel>
{


    private Text TimeMsg;

    public override void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        TimeMsg = UITool.FindChild<Text>(this.gameObject, "Text");
    }

    public void ShowTimeInfo(string time)
    {
        TimeMsg.text = time;
    }
}
