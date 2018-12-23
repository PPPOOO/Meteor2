using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCManager : MonoSingleton<NPCManager>
{
    private List<NPCInfo> NPCInfoList = new List<NPCInfo>();
    public Dictionary<int, string> HUDTextInfo = new Dictionary<int, string>();
    public Dictionary<int, string> TalkTextInfo = new Dictionary<int, string>();


    public List<NPCUI> AliveNPCList = new List<NPCUI>();
    public List<NPCUI> QuestNPCList = new List<NPCUI>();
    public Transform NPCPos;
    public bool ClickNPC = false;
    private PlayerStatus PS;

    protected override void Awake()
    {
        base.Awake();
        ParseHUDText();
        ParseTalkText();
        ParseNPCsInfo();
    }
    private void Start()
    {
        PS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        CheckDistance();
    }

    public NPCInfo GetNPCByID(int id)
    {
        foreach(NPCInfo npc in NPCInfoList)
        {
            if (npc.ID == id)
            {
                return npc;
            }
        }
        return null;
    }

    public void AddAliveNPCList(NPCUI npcui)
    {
        AliveNPCList.Add(npcui);
    }

    public void RemoveAliveNPCList(NPCUI npcui)
    {
        AliveNPCList.Remove(npcui);
    }

    public void AddQuestNPCList(NPCUI npcui)
    {
        QuestNPCList.Add(npcui);
    }

    public void RemoveQuestNPCList(NPCUI npcui)
    {
        QuestNPCList.Remove(npcui);
    }

    public void CheckDistance()
    {
        if (NPCPos == null) return;
        float distance = Vector3.Distance(PS.transform.position, NPCPos.position);
        if (distance > 3)
        {
            NPCTalkPanel.Instance.Hide();
        }
    }



    void ParseNPCsInfo()
    {
        TextAsset NPCText = Resources.Load<TextAsset>("NPCsInfo");
        string strNPCText = NPCText.text;
        JSONObject j = new JSONObject(strNPCText);
        foreach(JSONObject temp in j.list)
        {
            NPCInfo npcInfo = null;
            int id = (int)temp["id"].n;
            string name = temp["name"].str;
            string anim = temp["animation"].str;
            JSONObject j2 = temp["talktext"];
            List<string> talktext = new List<string>();
            foreach(JSONObject temp2 in j2.list)
            {
                int talkid = (int)temp2["talkid"].n;
                talktext.Add(TalkTextInfo[talkid]);
            }
            NPCInfo.NPCType type = (NPCInfo.NPCType)System.Enum.Parse(typeof(NPCInfo.NPCType), temp["type"].str);
            if(type== NPCInfo.NPCType.Normal)
            {
                npcInfo = new NPCInfo(id, name, anim, type, talktext);
            }
            else
            {
                JSONObject j3 = temp["hudtext"];
                List<string> hudtext = new List<string>();
                foreach (JSONObject temp3 in j3.list)
                {
                    int hudtextid = (int)temp3["hudtextid"].n;
                    hudtext.Add(HUDTextInfo[hudtextid]);
                }
                npcInfo = new NPCInfo(id, name, anim, type, talktext, hudtext);
            }
            NPCInfoList.Add(npcInfo);
        }
    }

    void ParseHUDText()
    {
        TextAsset HUDText = Resources.Load<TextAsset>("HUDTextInfo");
        string strHUDText = HUDText.text;
        JSONObject j =new JSONObject(strHUDText);
        foreach(JSONObject temp in j.list)
        {
            int id = (int)temp["id"].n;
            string content = temp[1].str;
            HUDTextInfo.Add(id,content);
        }
    }

    void ParseTalkText()
    {
        TextAsset TalkText = Resources.Load<TextAsset>("TalkTextInfo");
        string strTalkText = TalkText.text;
        JSONObject j = new JSONObject(strTalkText);
        foreach (JSONObject temp in j.list)
        {
            int id = (int)temp["id"].n;
            string content = temp[1].str;
            TalkTextInfo.Add(id, content);
        }
    }


}
