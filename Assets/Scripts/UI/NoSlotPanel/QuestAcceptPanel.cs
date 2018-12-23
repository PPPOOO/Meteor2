using UnityEngine;
using System.Collections;

public class QuestAcceptPanel : BasePanel<QuestAcceptPanel>
{

    private Transform Content;
    private GameObject QuestItemPrefab;

    public override void Start()
    {
        base.Start();
        Content = transform.Find("Scroll View/Viewport/AcceptQuestContent").transform;
        QuestItemPrefab = Resources.Load<GameObject>("Quest/QuestItem");
    }


    public override void Show()
    {
        base.Show();
        if (Content.childCount == 0)
        {
            for (int i = 0; i < QuestManager.Instance.QuestList.Count; i++)
            {
                GameObject go = Instantiate(QuestItemPrefab, Content, false);
                go.GetComponent<QuestItemUI>().SetID(i+1);
            }
        }
        
    }

    

}
