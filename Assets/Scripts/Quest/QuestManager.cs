using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoSingleton<QuestManager> {


    public List<Quest> QuestList;
    public List<QuestItemUI> AcceptQuestList = new List<QuestItemUI>();//任务
    public List<QuestItemUI> StartQuestList = new List<QuestItemUI>();//talk类任务才有
    public List<QuestItemUI> FinishQuestList = new List<QuestItemUI>();//
    public List<QuestItemUI> CanDeleteQuestList = new List<QuestItemUI>();

    public List<QuestItemUI> QuestItemUIsList = new List<QuestItemUI>();

    protected override void Awake()
    {
        base.Awake();
        ParseQuestInfo();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log(AcceptQuestList.Count);
            Debug.Log(StartQuestList.Count);
            Debug.Log(FinishQuestList.Count);
            Debug.Log(CanDeleteQuestList.Count);
        }
    }

    public void UpdateQuestShow()
    {

    }

    public Quest GetQuestByID(int id)
    {
        foreach(Quest quest in QuestList)
        {
            if (quest.ID == id)
            {
                return quest;
            }
        }
        return null;
    }

    #region 任务的状态变化监听
    public void AddAcceptQuestList(QuestItemUI questui)
    {
        AcceptQuestList.Add(questui);
        switch (questui.Quest.Questtype)
        {
            case Quest.QuestType.Combat:
                break;
            case Quest.QuestType.Talk:
                foreach(NPCUI npc in NPCManager.Instance.QuestNPCList)
                {
                    if (questui.Quest.StartNPCID == npc.ID)
                    {
                        npc.ShowQuestStatusIcon(questui);
                    }
                }
                questui.UpdateShowDes("找到" + NPCManager.Instance.GetNPCByID(questui.Quest.StartNPCID).Name);
                break;
            case Quest.QuestType.GetItem:
                foreach (NPCUI npc in NPCManager.Instance.QuestNPCList)
                {
                    if (questui.Quest.NPCID == npc.ID)
                    {
                        npc.ShowQuestStatusIcon(questui);
                    }
                }

                break;
            case Quest.QuestType.Work:
                break;
        }
    }
    public void RemoveAcceptQuestList(QuestItemUI questui)
    {
        AcceptQuestList.Remove(questui);
    }

    public void AddStartQuestList(QuestItemUI questui)
    {
        StartQuestList.Add(questui);
        RemoveAcceptQuestList(questui);
        foreach (NPCUI npc in NPCManager.Instance.QuestNPCList)
        {
            if (questui.Quest.NPCID == npc.ID)
            {
                npc.ShowQuestStatusIcon(questui);
            }
            
        }
        questui.UpdateShowDes("找到" + NPCManager.Instance.GetNPCByID(questui.Quest.NPCID).Name);

    }
    public void RemoveStartQuestList(QuestItemUI questui)
    {
        StartQuestList.Remove(questui);
    }

    public void AddFinishQuestList(QuestItemUI questui)
    {
        FinishQuestList.Add(questui);
        RemoveStartQuestList(questui);
        foreach (NPCUI npc in NPCManager.Instance.QuestNPCList)
        {
            if (questui.Quest.StartNPCID == npc.ID)
            {
                npc.ShowFinishIcon(questui);
            }
        }
        questui.UpdateShowDes("找到" + NPCManager.Instance.GetNPCByID(questui.Quest.StartNPCID).Name + "并拿到报酬");
    }
    public void RemoveFinishQuestList(QuestItemUI questui)
    {
        FinishQuestList.Remove(questui);
    }

    public void AddCanDeleteQuestList(QuestItemUI questui)
    {
        CanDeleteQuestList.Add(questui);
        RemoveFinishQuestList(questui);
        
    }
    public void RemoveCanDeleteQuestList(QuestItemUI questui)
    {
        CanDeleteQuestList.Remove(questui);
    }
    #endregion

    public void ParseQuestInfo()
    {
        QuestList = new List<Quest>();
        TextAsset QuestText = Resources.Load<TextAsset>("QuestsInfo");
        string questsJson = QuestText.text;
        JSONObject j = new JSONObject(questsJson);
        foreach (JSONObject temp in j.list)
        {
            int id = (int)temp["id"].n;
            string name = temp["name"].str;
            string des = temp["des"].str;
            int npcid = (int)temp["npcid"].n;

            JSONObject j2 = temp["reward"];
            int coin = (int)j2["coin"].n;
            int exp = (int)j2["exp"].n;
            QuestReward questReward = new QuestReward(coin, exp);
            Quest.QuestType type = (Quest.QuestType)System.Enum.Parse(typeof(Quest.QuestType), temp["type"].str);
            Quest quest = null;
            switch (type)
            {
                case Quest.QuestType.Combat:
                    quest = new Quest(id, name, des, type, npcid,questReward);
                    break;
                case Quest.QuestType.Talk:
                    int endnpcid = (int)temp["startnpcid"].n;
                    quest = new Quest(id, name, des, type, npcid, questReward, endnpcid);
                    break;
                case Quest.QuestType.GetItem:
                    int itemid = (int)temp["itemid"].n;
                    int count = (int)temp["count"].n;
                    quest = new Quest(id, name, des, type, npcid, questReward,itemid,count);
                    break;
                case Quest.QuestType.Work:
                    quest = new Quest(id, name, des, type, npcid, questReward);
                    break;
            }
            
            
            QuestList.Add(quest);
        }

    }
}
