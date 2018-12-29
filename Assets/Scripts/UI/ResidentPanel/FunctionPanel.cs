using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FunctionPanel : MonoBehaviour
{

    public Button StatusBtn;
    public Button BagBtn;
    public Button SkillBtn;
    public Button SettingBtn;

    public void OnClickStatusBtn()
    {
        StatusPanel.Instance.DisplaySwitch();
    }
    public void OnClickBagBtn()
    {
        Bag_PutOnPanel.Instance.DisplaySwitch();
    }
    public void OnClickSkillBtn()
    {
        SkillPanel.Instance.DisplaySwitch();
    }
    public void OnClickSettingBtn()
    {
        QuestPanel.Instance.DisplaySwitch();
    }

}
