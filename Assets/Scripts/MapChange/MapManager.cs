using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoSingleton<MapManager>
{
    public List<MapChangTrigger> mapChangTriggerList = new List<MapChangTrigger>();

    public Transform TwowayTrigger;

    public GameObject Playergameobject;
    public bool IsChangeMap = false;

    private void Start()
    {
        Playergameobject = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    private void Update()
    {
        
    }
}
