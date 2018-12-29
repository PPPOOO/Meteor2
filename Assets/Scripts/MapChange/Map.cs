using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Map : MonoBehaviour
{

    public List<NPCUI> NPCUIList = new List<NPCUI>();
    public NPCUI[] NPCUIs;

    private void Start()
    {
        NPCUIs =transform.Find("NPC").GetComponentsInChildren<NPCUI>();
    }

}
