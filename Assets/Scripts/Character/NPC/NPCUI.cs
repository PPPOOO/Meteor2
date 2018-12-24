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
    public List<QuestItemUI> NPCQuestuis = new List<QuestItemUI>();//随机任务的时候 一个npc身上有多种任务(发起人/中间人)方便管理
    public List<QuestItemUI> FinishQuestuiList = new List<QuestItemUI>();//



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
                if (FinishQuestuiList.Count <= 1)
                {
                    IsFinishQuest = false;
                }
                QuestManager.Instance.AddCanDeleteQuestList(FinishQuestuiList[FinishQuestuiList.Count - 1]);
                FinishQuestuiList.Remove(FinishQuestuiList[FinishQuestuiList.Count - 1]);
                HideFinishIcon();
                break;
            }

            else if (NPCTalkPanel.Instance.IsClickDoquest)
            {
                if (IsStartNPC == true)//跑腿任务 发起人
                {
                    NPCTalkContentPanel.Instance.ShowContent(NPCinfo.Name,"去找"+ NPCManager.Instance.GetNPCByID(NPCQuestuis[NPCQuestuis.Count - 1].Quest.NPCID).Name);
                    QuestManager.Instance.AddStartQuestList(NPCQuestuis[NPCQuestuis.Count - 1]);
                    if(NPCQuestuis.Count <= 1)
                    {
                        IsStartNPC = false;
                    }
                    NPCQuestuis.Remove(NPCQuestuis[NPCQuestuis.Count - 1]);
                    HideQuestIcon();
                }
                
                else
                {
                    NPCInfo startnPCInfo = NPCManager.Instance.GetNPCByID(NPCQuestuis[NPCQuestuis.Count - 1].Quest.StartNPCID);
                    NPCTalkContentPanel.Instance.ShowContent(NPCinfo.Name, "哦 我知道了。\n 你回去告诉" + startnPCInfo.Name + "吧。");
                    if (NPCQuestuis.Count <= 1)
                    {
                        IsQuestTarget = false;
                    }
                    HideQuestIcon();
                    QuestManager.Instance.AddFinishQuestList(NPCQuestuis[NPCQuestuis.Count - 1]);
                    NPCQuestuis.Remove(NPCQuestuis[NPCQuestuis.Count - 1]);
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


    public void ShowQuestStatusIcon(QuestItemUI questui)
    {
        NPCQuestuis.Add(questui);
        foreach(QuestItemUI qt in NPCQuestuis)
        {
            if (qt.Quest.Questtype == Quest.QuestType.Talk && qt.Quest.StartNPCID == ID)
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

    public void ShowFinishIcon(QuestItemUI questui)
    {
        FinishQuestuiList.Add(questui);
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
