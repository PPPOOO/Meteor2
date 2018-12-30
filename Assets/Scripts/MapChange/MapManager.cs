using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MapManager : MonoSingleton<MapManager>
{
    public List<MapChangTrigger> mapChangTriggerList = new List<MapChangTrigger>();

    public List<GameObject> MapsList = new List<GameObject>();
    public int SelectMapIndex = 0;

    public Transform TwowayTrigger;

    public GameObject Playergameobject;
    public bool IsChangeMap = false;

    private void Start()
    {
        Playergameobject = GameObject.FindGameObjectWithTag("Player").gameObject;
        MapsList = GameObject.FindGameObjectsWithTag("Map").ToList();
    }

    private void Update()
    {

    }


    public void UpdateMapShow()
    {
        MapsList[SelectMapIndex].SetActive(true);
        for (int i = 0; i < MapsList.Count; i++)
        {
            if (i != SelectMapIndex)
            {
                MapsList[i].SetActive(false);
            }
        }

    }

}
