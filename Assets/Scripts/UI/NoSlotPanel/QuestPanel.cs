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
        if (QuestManager.Instance.AcceptQuestList.Count == 0)
        {
            NoQuestText.gameObject.SetActive(true);
        }
        else
        {
            NoQuestText.gameObject.SetActive(false);
            foreach (Quest quest in QuestManager.Instance.AcceptQuestList)
            {
                if (ShowQuestLsit.Contains(quest)) continue;
                GameObject go = Instantiate(QuestItemPrefab, Content, false);
                go.GetComponent<QuestItemUI>().SetID(quest.ID);
                if (quest.ID == 1)
                {
                    Text des = UITool.FindChild<Text>(go, "QuestDes");
                    des.text = "消灭" + 1 + "/" + "3只史莱姆";
                }
                if (quest.ID == 2)
                {
                    Text des = UITool.FindChild<Text>(go, "QuestDes");
                    des.text = "消灭" + 1 + "/" + "5只蝙蝠";
                }
                ShowQuestLsit.Add(quest);
            }
            
            QuestItemUI[] questItemUIs = Content.GetComponentsInChildren<QuestItemUI>();
            foreach (Quest quest in QuestManager.Instance.FinishQuestList)
            {
                for(int i= questItemUIs.Length - 1; i > 0; i--)
                {
                    if( questItemUIs[i].ID == quest.ID)
                    {
                        Destroy(questItemUIs[i].gameObject);
                    }
                }
            }
        }
    }
}
