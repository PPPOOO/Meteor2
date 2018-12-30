using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsShelf : MonoBehaviour {

    public GameObject GoodsPrefab;
    //1.空地 选择作物种下 或者取消
    //2.已经有作物了
    //    A 作物没有成熟 选择拔出来（销毁）或者查看或者取消
    //    B 作物成熟了 选择查看或者取消（收获在管理站里进行。）
    private PlayerStatus mPlayerStatus;

    private void Start()
    {
        mPlayerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }


    private void OnMouseDown()
    {
        if (MonoBehaviourTool.Instance.GetOverUI() == true) return;
        if (transform.childCount <= 0)
        {
            if(BusinessManager.Instance.goods.Count < BusinessManager.Instance.mSellGoodsDayCount)
            {
                BusinessManager.Instance.GoodsShelftransform = transform;
                ChoiceGoodsPanel.Instance.Show();
            }
            else
            {
                ToolTip.Instance.ShowForTimeInMousePosition("摆放货物已达到每日最大交易量！！", 3);
                ToolTip.Instance.transform.position = Input.mousePosition;
            }
        }
        else if (transform.childCount >= 1 )
        {
            ToolTip.Instance.ShowForTimeInMousePosition("已经有货物啦！是否选择撤下这个货物 此功能暂时没有",2);
            ToolTip.Instance.transform.position = Input.mousePosition;
        }

    }

    public void ShowGoodsItem(int id)
    {
        GameObject cropgo = Instantiate(GoodsPrefab, this.transform, false);
        cropgo.transform.localPosition = Vector3.zero;
        cropgo.GetComponent<GoodsItem>().SetID(id);
    }



}
