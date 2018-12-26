using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quest 
{
    public int ID;
    public string Name;
    public string Des;
    public QuestType Questtype;
    public QuestReward QuestRewards;
    public Sprite TypeIcon;
    public QuestStatus Queststatus;
    public int NPCID;
    public int StartNPCID;
    public int ItemID;
    public int Count;
    public int KillCount;
    public int EnemyID;

    public Quest(int id,string name,string des,QuestType questType,int npcid, QuestReward questRewards)
    {
        ID = id;
        Name = name;
        Des = des;
        Questtype = questType;
        NPCID = npcid;
        QuestRewards = questRewards;
        SetTypeIcon();
        Queststatus = QuestStatus.None;
    }

    //杀敌任务
    public Quest(int id, string name, string des, QuestType questType, int npcid, int enemyid, int killcount ,QuestReward questRewards)
    {
        ID = id;
        Name = name;
        Des = des;
        Questtype = questType;
        NPCID = npcid;
        QuestRewards = questRewards;
        SetTypeIcon();
        EnemyID = enemyid;
        KillCount = killcount;
    }

    //跑腿任务
    public Quest(int id, string name, string des, QuestType questType, int npcid, QuestReward questRewards,int startnpcid)
    {
        ID = id;
        Name = name;
        Des = des;
        Questtype = questType;
        NPCID = npcid;
        QuestRewards = questRewards;
        StartNPCID = startnpcid;
        SetTypeIcon();
    }
    //得到物品任务
    public Quest(int id, string name, string des, QuestType questType, int npcid, QuestReward questRewards,int itemid,int count )
    {
        ID = id;
        Name = name;
        Des = des;
        Questtype = questType;
        NPCID = npcid;
        QuestRewards = questRewards;
        ItemID = itemid;
        Count = count;
        SetTypeIcon();
    }


    public void SetTypeIcon()
    {
        switch (Questtype)
        {
            case QuestType.Combat:
                TypeIcon = Resources.Load<Sprite>("Sprites/Quest/Combat");
                break;
            case QuestType.Talk:
                TypeIcon = Resources.Load<Sprite>("Sprites/Quest/Talk");
                break;
            case QuestType.GetItem:
                TypeIcon = Resources.Load<Sprite>("Sprites/Quest/GetItem");
                break;
            case QuestType.Work:
                TypeIcon = Resources.Load<Sprite>("Sprites/Quest/Work");
                break;
        }
    }
    
    public enum QuestType
    {
        Combat,
        Talk,
        GetItem,
        Work
    }

    public enum QuestStatus
    {
        None,
        Accept,
        Start,
        GoOn,
        Finish
    }



}
