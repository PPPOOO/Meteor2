using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    public List<Enemy> enemiesList = new List<Enemy>();
    public List<EnemyStatus> enemyUIsList = new List<EnemyStatus>();
    public GameObject EnemyPrefab;
    public Transform[] Spwans;
    public int EnenyCount = 15;

    protected override void Awake()
    {
        base.Awake();
        ParseEnemyInfo();
    }

    private void Start()
    {
        InvokeRepeating("CreatEneny", 0, 2);
    }

    public void CreatEneny()
    {
        if (enemyUIsList.Count < EnenyCount)
        {
            int randomSpawn = Random.Range(0, Spwans.Length);
            float randomx = Random.Range(-2, 2);
            float randomy = Random.Range(-2, 2);
            Vector3 randonpos = new Vector3(randomx, randomy);
            int randomEnemy = Random.Range(1, enemiesList.Count+1);
            GameObject go = Instantiate(EnemyPrefab, Spwans[randomSpawn], false);
            go.transform.position += randonpos;
            go.GetComponent<EnemyStatus>().SetID(randomEnemy);
        }
        else
        {
            CancelInvoke();
        }
    }

    public Enemy GetEnemyById(int id)
    {
        foreach(Enemy enemy in enemiesList)
        {
            if (enemy.ID == id)
            {
                return enemy;
            }
        }
        return null;
    }

    private void ParseEnemyInfo()
    {
        TextAsset enemyinfo = Resources.Load<TextAsset>("EnemyInfo");
        JSONObject j = new JSONObject(enemyinfo.text);
        foreach (JSONObject temp in j.list)
        {
            int id = (int)temp["id"].n;
            string name = temp["name"].str;
            string animation = temp["animation"].str;
            int gold = (int)temp["gold"].n;
            int exp = (int)temp["exp"].n;
            JSONObject j2 = temp["attr"];
            List<ApplyAttrEffect> applyAttrEffects = new List<ApplyAttrEffect>(); 
            foreach(JSONObject temp2 in j2.list)
            {
                AttrType attrType = (AttrType)System.Enum.Parse(typeof(AttrType), temp2["attrtype"].str);
                int value =(int) temp2["value"].n;
                ApplyAttrEffect applyAttrEffect = new ApplyAttrEffect(attrType, value);
                applyAttrEffects.Add(applyAttrEffect);
            }
            Enemy enemy = new Enemy(id, name, animation, gold, exp, applyAttrEffects);
            enemiesList.Add(enemy);
        }
    }
}
