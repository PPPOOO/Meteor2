using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestPanel : BasePanel<QuestPanel>
{
    private Text NoQuestText;
    private Transform Content;
    private GameObject QuestItemPrefab;
    private List<Quest> ShowQuestLsit = new List<Quest>();
    public override void Start()
    {
        base.Start();
        NoQuestText = UITool.FindChild<Text>(gameObject, "NoQuest");
        Content = transform.Find("Scroll View/Viewport/QuestContent").transform;
        QuestItemPrefab = Resources.Load<GameObject>("Quest/QuestItem");
    }


    public override void Show()
    {
        base.Show();
        UpdateQuestShow();
        
    }


    public void UpdateQuestShow()
    {
        if (QuestManager.Instance.AcceptQuestList.Count == 0&& QuestManager.Instance.StartQuestList.Count==0&& QuestManager.Instance.FinishQuestList.Count==0)
        {

            NoQuestText.gameObject.SetActive(true);
        }
        else
        {
            NoQuestText.gameObject.SetActive(false);
            
        }
        QuestItemUI[] questItemUIs = Content.GetComponentsInChildren<QuestItemUI>();
        foreach (QuestItemUI questui in QuestManager.Instance.CanDeleteQuestList)
        {

            for (int i = questItemUIs.Length - 1; i >= 0; i--)
            {
                if (questItemUIs[i].ID == questui.ID)
                {
                    Destroy(questItemUIs[i].gameObject);
                }
            }
        }
    }
}
