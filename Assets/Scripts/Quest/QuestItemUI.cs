﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestItemUI : MonoBehaviour,IPointerDownHandler
{
    public int ID;
    private Text Name;
    private Image TypeIcon;
    private Text Des;
    private Text Reward;
    private Quest Quest;


    public void SetID(int id)
    {
        Name = UITool.FindChild<Text>(gameObject, "Name");
        TypeIcon = UITool.FindChild<Image>(gameObject, "TypeIcon");
        Des = UITool.FindChild<Text>(gameObject, "QuestDes");
        Reward = UITool.FindChild<Text>(gameObject, "RewardDes");

        ID = id;
        Quest= QuestManager.Instance.GetQuestByID(id);
        Name.text = Quest.Name;
        TypeIcon.sprite = Quest.TypeIcon;
        Des.text = Quest.Des;
        Reward.text = Quest.QuestRewards.Coin + "金币  " + Quest.QuestRewards.Exp + "经验";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (transform.parent.name == "AcceptQuestContent")
            {
                ConfirmPanel.Instance.Show();
                StartCoroutine(AcceptQuestConfirm(ID));
            }
            else if(transform.parent.name == "QuestContent")
            {

            }
        }
        
    }



    IEnumerator AcceptQuestConfirm(int id)
    {
        while (1 > 0)
        {
            yield return new WaitForSeconds(0.05f);
            if (ConfirmPanel.Instance.IsClickOK)
            {
                QuestManager.Instance.AddAcceptQuestList(Quest);
                
                Destroy(gameObject);
                break;
            }
            if (ConfirmPanel.Instance.IsClickCancel)
            {
                break;
            }
        }
    }


}