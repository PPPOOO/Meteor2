using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCUI :MonoBehaviour
{
    public int ID;
    public NPCInfo NPCinfo=null;
    public NPCInfo.NPCType NPCtype;
    protected Animator animator;
    public float MoveSpeed = 1;
    public int i = -1;
    public Vector3 RandomDir;
    public Vector3 RandomTargetPos;
    private GameObject HUDPrefab;
    private Transform HUDPanel;




    public bool IsQuestTarget = false;
    public bool IsStartNPC = false;
    public bool IsFinishQuest = false;
    public List<Quest> NPCQuests = new List<Quest>();
    public List<Quest> FinishQuestList = new List<Quest>();



    public virtual void SetID(int id)
    {
        HUDPrefab = Resources.Load<GameObject>("HUDText");
        HUDPanel = GameObject.FindGameObjectWithTag("Canvas").transform.Find("HUDPanel");
        animator = GetComponent<Animator>();
        
        ID = id;
        NPCinfo = NPCManager.Instance.GetNPCByID(ID);
        NPCManager.Instance.AddAliveNPCList(this);
        NPCManager.Instance.AddQuestNPCList(this);
        NPCtype = NPCinfo.NPCtype;
        GetAnimation(NPCinfo.Animation);
        if (NPCtype != NPCInfo.NPCType.Quest)
        {
            RandomMove();
        }
        float randomTime = Random.Range(0, 3);
        InvokeRepeating("ShowHUDText", randomTime, 3);
    }




    public  void OnMouseDown()
    {
        if (MonoBehaviourTool.Instance.GetOverUI() == false)
        {
            NPCManager.Instance.NPCPos = this.transform;
            NPCTalkPanel.Instance.Show();
            StopAllCoroutines();
            StartCoroutine(OnTalk());
            if (IsQuestTarget == true||IsStartNPC==true)
            {
                NPCTalkPanel.Instance.DoQuest.gameObject.SetActive(true);
            }
            if (IsFinishQuest == true)
            {
                NPCTalkPanel.Instance.FinishQuest.gameObject.SetActive(true);
            }
            switch (NPCinfo.NPCtype)
            {
                case NPCInfo.NPCType.Normal:
                    break;
                case NPCInfo.NPCType.Shop:
                    NPCTalkPanel.Instance.Business.gameObject.SetActive(true);
                    break;
                case NPCInfo.NPCType.Quest:
                    NPCTalkPanel.Instance.AcceptQuest.gameObject.SetActive(true);
                    break;
            }
        }
    }



    IEnumerator OnTalk()
    {
        while (1 > 0)
        {
            yield return new WaitForSeconds(0.05f);
            if (NPCTalkPanel.Instance.IsClickFinshQuest)
            {
                NPCTalkContentPanel.Instance.ShowContent(NPCinfo.Name, "多谢了!");
                if (FinishQuestList.Count <= 1)
                {
                    IsFinishQuest = false;
                }
                FinishQuestList.Remove(FinishQuestList[FinishQuestList.Count - 1]);
                HideFinishIcon();
                break;
            }

            else if (NPCTalkPanel.Instance.IsClickDoquest)
            {
                if (IsStartNPC == true)//跑腿任务 发起人
                {
                    NPCTalkContentPanel.Instance.ShowContent(NPCinfo.Name, NPCQuests[NPCQuests.Count - 1].Des);
                    QuestManager.Instance.AddStartQuestList(NPCQuests[NPCQuests.Count - 1]);
                    //QuestManager.Instance.RemoveAcceptQuestList(NPCQuests[NPCQuests.Count - 1]);
                    
                    if(NPCQuests.Count <= 1)
                    {
                        IsStartNPC = false;
                    }
                    NPCQuests.Remove(NPCQuests[NPCQuests.Count - 1]);
                    HideQuestIcon();
                }
                
                else
                {
                    NPCInfo startnPCInfo = NPCManager.Instance.GetNPCByID(NPCQuests[NPCQuests.Count - 1].StartNPCID);
                    NPCTalkContentPanel.Instance.ShowContent(NPCinfo.Name, "哦 我知道了。\n 你回去告诉" + startnPCInfo.Name + "吧。");
                    if (NPCQuests.Count <= 1)
                    {
                        IsQuestTarget = false;
                    }
                    
                    HideQuestIcon();
                    QuestManager.Instance.AddFinishQuestList(NPCQuests[NPCQuests.Count - 1]);
                    NPCQuests.Remove(NPCQuests[NPCQuests.Count - 1]);
                    //foreach (Quest qt in NPCQuests)
                    //{

                    //    NPCInfo startnPCInfo = NPCManager.Instance.GetNPCByID(qt.StartNPCID);
                    //    NPCTalkContentPanel.Instance.ShowContent(NPCinfo.Name, "哦 我知道了。\n 你回去告诉" + startnPCInfo.Name + "吧。");
                    //    IsQuestTarget = false;
                    //    HideQuestIcon();
                    //    QuestManager.Instance.AddFinishQuestList(qt);


                    //}

                }
                
                break;
            }
            else if (NPCTalkPanel.Instance.IsClickAcceptQuest)
            {
                QuestAcceptPanel.Instance.Show();
                break;
            }
            else if (NPCTalkPanel.Instance.IsClickBusiness)
            {
                ShopPanel.Instance.Show();
                break;
            }
            else if (NPCTalkPanel.Instance.IsClickTalk)
            {
                ToolTip.Instance.ShowForTimeInMousePosition("当前该功能未开放！",2);
                break;
            }
            else if (NPCTalkPanel.Instance.IsClickQuit)
            {
                break;
            }
        }
    }


    public void ShowQuestStatusIcon(Quest quest)
    {
        NPCQuests.Add(quest);
        foreach(Quest qt in NPCQuests)
        {
            if (qt.Questtype == Quest.QuestType.Talk && qt.StartNPCID == ID)
            {
                IsStartNPC = true;
            }
            else
            {
                IsQuestTarget = true;
            }
        }
        
        
        GameObject questIconPrefab = Resources.Load<GameObject>("Quest/QuestStatusIcon");
        GameObject go = Instantiate(questIconPrefab, transform, false);
        go.transform.localPosition = Vector3.zero + Vector3.up;
    }

    public void ShowFinishIcon(Quest quest)
    {
        FinishQuestList.Add(quest);
        IsFinishQuest = true;
        GameObject questIconPrefab = Resources.Load<GameObject>("Quest/TargetFinish");
        GameObject go = Instantiate(questIconPrefab, transform, false);
    }

    public void HideQuestIcon()
    {
        Destroy(transform.Find("QuestStatusIcon(Clone)").gameObject);
    }

    public void HideFinishIcon()
    {
        Destroy(transform.Find("TargetFinish(Clone)").gameObject);
    }


    public void ShowHUDText()
    {
        if (NPCtype == NPCInfo.NPCType.Normal) return;
        GameObject go = Instantiate(HUDPrefab, HUDPanel, false);
        
        int randomnum = Random.Range(0, NPCinfo.HUDText.Count);
        go.GetComponent<HUDText>().target = this.transform;
       go.GetComponent<HUDText>().ShowHUDText(NPCinfo.HUDText[randomnum]);
    }

    public virtual void OnMouseEnter()
    {
        CursorManager.Instance.SetNPCTalk();
    }
    public virtual void OnMouseExit()
    {
        CursorManager.Instance.SetNormal();
    }

    #region 随机上下左右移动和得到动画
    public void RandomMove()
    {
        InvokeRepeating("GetRandomTargetPos", 0, 5);
        InvokeRepeating("MoveToRandomTargetPos", 0, 0.02f);
    }


    public void GetRandomTargetPos()
    {
        i = Random.Range(0, 4);
        if (i == 0)
        {
            RandomDir = new Vector3(0, 1);
        }
        if (i == 1)
        {
            RandomDir = new Vector3(0, -1);
        }
        if(i==2)
        {
            RandomDir = new Vector3(1, 0);
        }
        if (i == 3)
        {
            RandomDir = new Vector3(-1, 0);
        }
        animator.SetFloat("x", RandomDir.x);
        animator.SetFloat("y", RandomDir.y);
        RandomTargetPos = transform.position + RandomDir;
    }

    public void MoveToRandomTargetPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, RandomTargetPos, MoveSpeed * Time.deltaTime);
    }

    public void GetAnimation(string NPC_Resources_Name)
    {
        string AnimationPath = "Animation/Animation/";
        animator = GetComponent<Animator>();
        AnimatorOverrideController overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;
        overrideController["Up"] = Resources.Load<AnimationClip>(AnimationPath+NPC_Resources_Name+"/Up");
        overrideController["Right"] = Resources.Load<AnimationClip>(AnimationPath + NPC_Resources_Name +"/Right");
        overrideController["Left"] = Resources.Load<AnimationClip>(AnimationPath + NPC_Resources_Name +"/Left"); ;
        overrideController["Down"] = Resources.Load<AnimationClip>(AnimationPath + NPC_Resources_Name +"/Down");

    }
    #endregion
}
