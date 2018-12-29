using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{

    private int mID;
    private SkillBaseInfo mInfo;
    private Image mIcon;
    private Text mName;
    private Text mDes;
    private Text mCost;
    private Image mIcon_Mask;



    public int ID { get { return mID; } }
    public SkillBaseInfo Info { get { return mInfo; } }
    public Image Icon { get { return mIcon; } }
    public Text Name { get { return mName; } }


    public void SetID(int id)
    {
        mIcon = UITool.FindChild<Image>(gameObject, "Icon");
        mName = UITool.FindChild<Text>(gameObject, "Name");
        mDes = UITool.FindChild<Text>(gameObject, "Des");
        mCost = UITool.FindChild<Text>(gameObject, "Cost");
        mIcon_Mask = UITool.FindChild<Image>(gameObject, "Icon_Mask");
        mIcon_Mask.enabled = false;

        mInfo = SkillManager.Instance.GetSkillByID(id);
        mIcon.sprite = Resources.Load<Sprite>(mInfo.Sprite);
        mName.text = mInfo.Name;
        mDes.text = mInfo.Des;
        mCost.text = "ep" + mInfo.EP + " mp" + mInfo.MP;
    }

    public void UpdateCanUse(int level)
    {
        if (Info.DemandLv <= level)
        {
            mIcon_Mask.enabled = false;
            mIcon.gameObject.GetComponent<SkillIconDrag>().enabled = true;
        }
        else
        {
            mIcon_Mask.enabled = true;
            mIcon.gameObject.GetComponent<SkillIconDrag>().enabled = false;
        }
    }

}
