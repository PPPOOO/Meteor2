using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CrolLand : MonoBehaviour
{

    public GameObject CropPrefab;
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
            CropManager.Instance.Croplandtransform = transform;
            ChoiceCropPanel.Instance.Show();
        }
        else if(transform.childCount >= 1&&transform.GetChild(0).GetComponent<CropItem>().IsGrown==false)
        {

            ToolTip.Instance.ShowFollowMouse("该作物已经种下，暂不可销毁和查看！");
            ToolTip.Instance.transform.position = Input.mousePosition;
        }
        else if(transform.childCount >= 1 && transform.GetChild(0).GetComponent<CropItem>().IsGrown == true)
        {
            ToolTip.Instance.ShowFollowMouse("该作物已经成熟，收获请到管理站！");
            ToolTip.Instance.transform.position = Input.mousePosition;
        }
    }

    public void Plant(int id)
    {
        GameObject cropgo= Instantiate(CropPrefab, this.transform, false);
        cropgo.transform.localPosition = Vector3.zero;
        cropgo.GetComponent<CropItem>().SetID(id);
    }

    private void OnMouseExit()
    {
        ToolTip.Instance.Hide();
    }

}
