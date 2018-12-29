using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapPanel : BasePanel<MiniMapPanel>
{

    private Camera mMinmiMapCamera;
    private Button mPlusBtn;
    private Button mMinusBtn;


    public override void Start()
    {
        mMinmiMapCamera = GameObject.FindGameObjectWithTag("Player").transform.Find("MiniMapCamera").GetComponent<Camera>();
        mPlusBtn = UITool.FindChild<Button>(gameObject, "UpBtn");
        mMinusBtn = UITool.FindChild<Button>(gameObject, "DownBtn");


        mPlusBtn.onClick.AddListener(() => {
            mMinmiMapCamera.orthographicSize--;
            if (mMinmiMapCamera.orthographicSize <= 5)
                mMinmiMapCamera.orthographicSize = 5;
        });
        mMinusBtn.onClick.AddListener(() => { mMinmiMapCamera.orthographicSize++;
            if (mMinmiMapCamera.orthographicSize >= 10)
             mMinmiMapCamera.orthographicSize = 10; });
    }


}