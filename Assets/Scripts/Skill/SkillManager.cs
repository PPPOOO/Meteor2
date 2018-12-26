using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillManager : MonoBehaviour
{
    private static SkillManager _instance;

    public static SkillManager Instance
    {
        get
        {
            if (_instance == null)
            {
                //下面的代码只会执行一次
                _instance = GameObject.FindGameObjectWithTag("Manager").GetComponent<SkillManager>();
            }
            return _instance;
        }
    }


    //public GameObject SkillItem;
    //private Transform mContent;
    private Dictionary<int, SkillBaseInfo> mSkillInfoDict;
    //private SkillUI[] mSkillList;
    //private PlayerStatus mPS;

    //private void Awake()
    //{
    //    ParseSkillJson();
    //}
    //// Use this for initialization
    //void Start()
    //{
    //    mPS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    //    mContent = GameObject.FindGameObjectWithTag("Canvas").transform.Find("SkillPanel(Clone)/Scroll View/Viewport/Content");
    //    foreach (KeyValuePair<int, SkillBaseInfo> kvp in mSkillInfoDict)
    //    {
    //        GameObject skillgo = Instantiate(SkillItem, mContent, false);
    //        skillgo.GetComponent<SkillUI>().SetID(kvp.Key);
    //    }
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    //public SkillBaseInfo GetSkillByID(int id)
    //{
    //    SkillBaseInfo info = null;
    //    mSkillInfoDict.TryGetValue(id, out info);
    //    if (info == null)
    //    {
    //        Debug.LogError("没有id为" + id + "的技能!");
    //    }
    //    return info;
    //}

    //public void UpdateSkillCanUse()
    //{
    //    mSkillList = GameObject.FindGameObjectWithTag("Canvas").transform.Find("SkillPanel(Clone)/Scroll View/Viewport/Content").GetComponentsInChildren<SkillUI>();
    //    foreach (SkillUI skillItem in mSkillList)
    //    {
    //        skillItem.UpdateCanUse(mPS.Level);
    //    }
    //}


    void ParseSkillJson()
    {

        mSkillInfoDict = new Dictionary<int, SkillBaseInfo>();
        //文本为在Unity里面是 TextAsset类型
        TextAsset skillText = Resources.Load<TextAsset>("Test");
        string itemsJson = skillText.text;//物品信息的Json格式
        JSONObject j = new JSONObject(itemsJson);
        foreach (JSONObject temp in j.list)
        {
            int id = (int)temp["id"].n;
            string name = temp["name"].str;
            string sprite = temp["sprite"].str;
            string des = temp["des"].str;
            int mp = (int)temp["mp"].n;
            int ep = (int)temp["ep"].n;
            int demandlv = (int)temp["demandlv"].n;
            float cooltime = temp["coolTime"].n;
            string str_releasetype = temp["releaseType"].str;
            SkillBaseInfo.ReleaseType releaseType = (SkillBaseInfo.ReleaseType)System.Enum.Parse(typeof(SkillBaseInfo.ReleaseType), str_releasetype);

            List<ApplyAttrEffect> applyAttrEffects = new List<ApplyAttrEffect>();
            JSONObject j2 = temp["applyAttr"];
            foreach (JSONObject temp2 in j2.list)
            {
                AttrType attrType = (AttrType)System.Enum.Parse(typeof(AttrType), temp2["attrTpye"].str);
                int fixvalue = (int)temp2["fixValue"].n;
                List<AddAttrValue> addAttrValues = new List<AddAttrValue>();
                JSONObject j3 = temp2["addValue"];
                foreach (JSONObject temp3 in j3.list)
                {
                    AttrType addattrtype = (AttrType)System.Enum.Parse(typeof(AttrType), temp3["addAttrType"].str);
                    float ap = temp3["addpoint"].n;
                    AddAttrValue add = new AddAttrValue(addattrtype, ap);
                    addAttrValues.Add(add);
                }
                int count = 0;
                if (temp2["count"] != null)
                {
                    count = (int)temp2["count"].n;
                }
                float time = 0;
                if (temp2["time"] != null)
                {
                    time = temp2["time"].n;
                }
                ApplyAttrEffect attrEffect = new ApplyAttrEffect(attrType, fixvalue, addAttrValues, time, count);
                applyAttrEffects.Add(attrEffect);
            }

            SkillBaseInfo skill = null;
            switch (releaseType)
            {
                case SkillBaseInfo.ReleaseType.Self:

                    skill = new SkillSelf(id, name, sprite, des, mp, ep, demandlv, cooltime, releaseType, applyAttrEffects);
                    break;
                case SkillBaseInfo.ReleaseType.SelfRange:
                    float selfRange = temp["range"].n;
                    skill = new SkillSelfRange(id, name, sprite, des, mp, ep, demandlv, cooltime, releaseType, selfRange, applyAttrEffects);
                    break;
                case SkillBaseInfo.ReleaseType.Multi:
                    float multiDistance = temp["distance"].n;
                    float multiRange = temp["range"].n;
                    skill = new SkillMulti(id, name, sprite, des, mp, ep, demandlv, cooltime, releaseType, multiRange, multiDistance, applyAttrEffects);
                    break;
                case SkillBaseInfo.ReleaseType.Single:
                    float singleDistance = temp["distance"].n;
                    skill = new SkillSingle(id, name, sprite, des, mp, ep, demandlv, cooltime, releaseType, singleDistance, applyAttrEffects);
                    break;
                case SkillBaseInfo.ReleaseType.Trajectory:
                    float shotSize = temp["shotSize"].n;
                    float shotSpeed = temp["shotSpeed"].n;
                    float shotTime = temp["shotTime"].n;
                    bool pierce = temp["pierce"].IsBool;
                    skill = new SkillTrajectory(id, name, sprite, des, mp, ep, demandlv, cooltime, releaseType, shotSize, shotSpeed, shotTime, pierce, applyAttrEffects);
                    break;
            }
            mSkillInfoDict.Add(id, skill);
        }

        }
}

