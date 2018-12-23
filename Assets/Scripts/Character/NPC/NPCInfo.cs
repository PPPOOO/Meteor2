using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCInfo 
{
    public int ID;
    public string Name;
    public string Animation;
    public NPCType NPCtype;
    public List<string> TalkText;
    public List<string> HUDText;


    public NPCInfo(int id,string name,string animation,NPCType nPCType,List<string> talktext)
    {
        ID = id;
        Name = name;
        Animation = animation;
        NPCtype = nPCType;
        TalkText = talktext;
    }
    public NPCInfo(int id, string name, string animation, NPCType nPCType, List<string> talktext,List<string> hudtext)
    {
        ID = id;
        Name = name;
        Animation = animation;
        NPCtype = nPCType;
        TalkText = talktext;
        HUDText = hudtext;
    }


    public enum NPCType
    {
        Normal,
        Shop,
        Quest
    }
}
