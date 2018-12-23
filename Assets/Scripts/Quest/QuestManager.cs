using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoSingleton<QuestManager> {


    public List<Quest> QuestList;
    public List<Quest> AcceptQuestList = new List<Quest>();//任务
    public List<Quest> StartQuestList = new List<Quest>();//talk类任务才有
    public List<Quest> FinishQuestList = new List<Quest>();//

    protected override void Awake()
    {
        base.Awake();
        ParseQuestInfo();
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


    public void AddAcceptQuestList(Quest quest)
    {
        AcceptQuestList.Add(quest);
        switch (quest.Questtype)
        {
            case Quest.QuestType.Combat:
                break;
            case Quest.QuestType.Talk:
                foreach(NPCUI npc in NPCManager.Instance.QuestNPCList)
                {
                    if (quest.StartNPCID == npc.ID)
                    {
                        npc.ShowQuestStatusIcon(quest);
                    }
                }
                break;
            case Quest.QuestType.GetItem:
                break;
            case Quest.QuestType.Work:
                break;
        }
    }
    public void RemoveAcceptQuestList(Quest quest)
    {
        AcceptQuestList.Remove(quest);
    }

    public void AddStartQuestList(Quest quest)
    {
        StartQuestList.Add(quest);
        foreach (NPCUI npc in NPCManager.Instance.QuestNPCList)
        {
            if (quest.NPCID == npc.ID)
            {
                npc.ShowQuestStatusIcon(quest);
            }
        }

    }
    public void RemoveStartQuestList(Quest quest)
    {
        StartQuestList.Remove(quest);
    }

    public void AddFinishQuestList(Quest quest)
    {
        FinishQuestList.Add(quest);
        foreach (NPCUI npc in NPCManager.Instance.QuestNPCList)
        {
            if (quest.StartNPCID == npc.ID)
            {
                npc.ShowFinishIcon(quest);
            }
        }
    }
    public void RemoveFinishQuestList(Quest quest)
    {
        FinishQuestList.Remove(quest);
    }


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
