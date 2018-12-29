using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillSenseTra : MonoBehaviour
{
    public float mSpeed;
    private CircleCollider2D mCircleCol;
    private GameObject SelfGameObject;
    private List<CharacetStatus> characetStatuses = new List<CharacetStatus>();
    private int TestObjectMode = 0;
    private SkillBaseInfo mSkillBaseInfo;

    private void Start()
    {
        mCircleCol = GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        transform.Translate(gameObject.transform.up * Time.deltaTime * mSpeed);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (TestObjectMode == 1)
        {
            if (col.tag == "Player" || col.tag == "Pet")
            {
                CharacetStatus characetStatus = col.GetComponent<CharacetStatus>();
                int index = characetStatuses.IndexOf(characetStatus);
                if (index == -1)
                {
                    SkillManager.Instance.CaleSkillAttrObjectValue(SelfGameObject, col.gameObject, mSkillBaseInfo);
                    characetStatuses.Add(characetStatus);
                }
            }
        }

        if (TestObjectMode == 2)
        {
            if (col.tag == "Enemy")
            {
                CharacetStatus characetStatus = col.GetComponent<CharacetStatus>();
                int index = characetStatuses.IndexOf(characetStatus);
                if (index == -1)
                {
                    SkillManager.Instance.CaleSkillAttrObjectValue(SelfGameObject, col.gameObject, mSkillBaseInfo);
                    characetStatuses.Add(characetStatus);
                }
            }
        }

    }



    public void JudgeObejct(GameObject gameObject, SkillBaseInfo skillBaseInfo)
    {
        if (gameObject.tag == "Player" || gameObject.tag == "Pet")
        {
            if (skillBaseInfo.Releaseobject == SkillBaseInfo.ReleaseObject.Ally)
            {
                TestObjectMode = 1;
            }
            else
            {
                TestObjectMode = 2;
            }
        }
        if (gameObject.tag == "Enemy")
        {
            if (skillBaseInfo.Releaseobject == SkillBaseInfo.ReleaseObject.Ally)
            {
                TestObjectMode = 2;
            }
            else
            {
                TestObjectMode = 1;
            }
        }
        SelfGameObject = gameObject;
        mSkillBaseInfo = skillBaseInfo;
    }

}
