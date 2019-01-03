using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoSingleton<QuestManager> {
    
    public List<Quest> QuestList;
    public List<QuestItemUI> AcceptQuestList = new List<QuestItemUI>();//任务
    public List<QuestItemUI> StartQuestList = new List<QuestItemUI>();//talk类任务才有
    public List<QuestItemUI> FinishQuestList = new List<QuestItemUI>();//
    public List<QuestItemUI> CanDeleteQuestList = new List<QuestItemUI>();
    


    public List<QuestItemUI> KillEnemyList = new List<QuestItemUI>();
    public bool IsKillEnemyQuest = false;

    public List<QuestItemUI> QuestItemUIList = new List<QuestItemUI>();
    public bool IsItemQuest = false;

    protected override void Awake()
    {
        base.Awake();
        ParseQuestInfo();
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
                IsKillEnemyQuest = true;
                KillEnemyList.Add(questui);
                foreach (NPCUI npc in NPCManager.Instance.QuestNPCList)
                {
                    if (questui.Quest.NPCID == npc.ID)
                    {
                        npc.ShowQuestStatusIcon(questui);
                    }
                }
                questui.UpdateShowDes(questui.CurrentKillCount);
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
                IsItemQuest = true;
                InventoryManager.Instance.GetItemQuest(questui);

                if (QuestItemUIList.Contains(questui) == false)
                {
                    QuestItemUIList.Add(questui);
                }
                foreach (NPCUI npc in NPCManager.Instance.QuestNPCList)
                {
                    if (questui.Quest.NPCID == npc.ID)
                    {
                        if (questui.CurrentCount >= questui.Quest.Count)
                        {
                            AddFinishQuestList(questui);
                        }
                        else
                        {
                            npc.ShowQuestStatusIcon(questui);
                        }
                    }
                }
                questui.UpdateShowDes(questui.CurrentCount);
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
        if (questui.Quest.Questtype == Quest.QuestType.Talk)
        {
            RemoveStartQuestList(questui);
        }
        else
        {
            RemoveAcceptQuestList(questui);
        }
        if(questui.Quest.Questtype == Quest.QuestType.Talk)
        {
            foreach (NPCUI npc in NPCManager.Instance.QuestNPCList)
            {
                if (questui.Quest.StartNPCID == npc.ID)
                {

                    npc.ShowFinishIcon(questui);
                    
                }
            }
            questui.UpdateShowDes("找到" + NPCManager.Instance.GetNPCByID(questui.Quest.StartNPCID).Name + "并拿到报酬");
        }
        else 
        {
            foreach (NPCUI npc in NPCManager.Instance.QuestNPCList)
            {
                if (questui.Quest.NPCID == npc.ID)
                {
                    npc.ShowFinishIcon(questui);
                }
            }
            questui.UpdateShowDes("找到" + NPCManager.Instance.GetNPCByID(questui.Quest.NPCID).Name + "并拿到报酬");
        }


    }
    public void RemoveFinishQuestList(QuestItemUI questui)
    {
        FinishQuestList.Remove(questui);
    }

    public void AddCanDeleteQuestList(QuestItemUI questui)
    {
        
        if (QuestItemUIList.Contains(questui))
        {
            if (QuestItemUIList.Count <= 1)
            {
                IsItemQuest = false;
            }
            QuestItemUIList.Remove(questui);
        }
        if (KillEnemyList.Contains(questui))
        {
            if (KillEnemyList.Count <= 1)
            {
                IsKillEnemyQuest = false;
            }
            KillEnemyList.Remove(questui);
        }

        CanDeleteQuestList.Add(questui);
        RemoveFinishQuestList(questui);
    }
    public void RemoveCanDeleteQuestList(QuestItemUI questui)
    {
        CanDeleteQuestList.Remove(questui);
    }
    #endregion

    public void EnemyKilled(EnemyStatus enemyStatus)
    {
        if (IsKillEnemyQuest == false) return;
        else
        {
            foreach (QuestItemUI questItemUI in KillEnemyList)
            {
                if (questItemUI.Quest.EnemyID == enemyStatus.ID)
                {
                    questItemUI.CurrentKillCount++;
                    questItemUI.UpdateShowDes(questItemUI.CurrentKillCount);
                    if (questItemUI.CurrentKillCount >= questItemUI.Quest.KillCount && FinishQuestList.Contains(questItemUI) == false)
                    {
                        AddFinishQuestList(questItemUI);
                    }
                }
            }
        }
    }

    public void GetQuestItem(Item changeitem, int changecount = 1)
    {
        for (int i = 0; i < QuestItemUIList.Count; i++)
        {
            if (changeitem.ID == QuestItemUIList[i].Quest.ItemID)
            {
                QuestItemUIList[i].CurrentCount += changecount;
                if (QuestItemUIList[i].CurrentCount <= 0)
                {
                    QuestItemUIList[i].CurrentCount = 0;
                }
                QuestItemUIList[i].UpdateShowDes(QuestItemUIList[i].CurrentCount);
                NPCUI npcUI = null;
                foreach (NPCUI npc in NPCManager.Instance.QuestNPCList)
                {
                    if (QuestItemUIList[i].Quest.NPCID == npc.ID)
                    {
                        npcUI = npc;
                    }
                }
                if (QuestItemUIList[i].CurrentCount >= QuestItemUIList[i].Quest.Count && FinishQuestList.Contains(QuestItemUIList[i]) == false)
                {
                    AddFinishQuestList(QuestItemUIList[i]);
                }
                if (QuestItemUIList[i].CurrentCount < QuestItemUIList[i].Quest.Count && FinishQuestList.Contains(QuestItemUIList[i]) == true)
                {
                    AddAcceptQuestList(QuestItemUIList[i]);
                    RemoveFinishQuestList(QuestItemUIList[i]);
                    if (npcUI.FinishQuestuiList.Count <= 1)
                    {
                        npcUI.IsFinishQuest = false;
                    }
                    npcUI.FinishQuestuiList.Remove(npcUI.FinishQuestuiList[npcUI.FinishQuestuiList.Count - 1]);
                    npcUI.HideFinishIcon();
                }
            }
        }
    }

    public void ParseQuestInfo()
    {
        QuestList = new List<Quest>();
        TextAsset QuestText = Resources.Load<TextAsset>("Json/QuestsInfo");
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
                    int enemyid = (int)temp["enemyid"].n;
                    int killcount = (int)temp["killcount"].n;
                    quest = new Quest(id, name, des, type, npcid,enemyid,killcount, questReward);
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
