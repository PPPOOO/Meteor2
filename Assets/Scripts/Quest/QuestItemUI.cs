using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestItemUI : MonoBehaviour,IPointerDownHandler
{
    public int ID;
    private Text Name;
    private Image TypeIcon;
    public Text Des;
    public Text Reward;
    public Quest Quest;
    public int CurrentCount=0;
    public int CurrentKillCount = 0;


    public void SetID(int id)
    {
        Name = UITool.FindChild<Text>(gameObject, "Name");
        TypeIcon = UITool.FindChild<Image>(gameObject, "TypeIcon");
        Des = UITool.FindChild<Text>(gameObject, "QuestDes");
        Reward = UITool.FindChild<Text>(gameObject, "RewardDes");

        ID = id;
        Quest= QuestManager.Instance.GetQuestByID(id);
        Name.text = Quest.Name;
        TypeIcon.sprite = Quest.TypeIcon;
        Des.text = Quest.Des;
        Reward.text = Quest.QuestRewards.Coin + "金币  " + Quest.QuestRewards.Exp + "经验";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (transform.parent.name == "AcceptQuestContent")
            {
                ConfirmPanel.Instance.Show();
                StartCoroutine(AcceptQuestConfirm(ID));
            }
            else if(transform.parent.name == "QuestContent")
            {

            }
        }
    }



    public void UpdateShowDes(string des)
    {
        Des.text = des;
    }

    public void UpdateShowDes(int count)
    {
        
        if (Quest.Questtype == Quest.QuestType.GetItem)
        {
            Des.text = (NPCManager.Instance.GetNPCByID(Quest.NPCID)).Name + " " + count + "/" + Quest.Count + " " + (InventoryManager.Instance.GetItemById(Quest.ItemID).Name);
        }
       else if(Quest.Questtype == Quest.QuestType.Combat)
        {
            Des.text = EnemyManager.Instance.GetEnemyById(Quest.EnemyID).Name + " " + count + "/" + Quest.KillCount;
        }
    }

    

    IEnumerator AcceptQuestConfirm(int id)
    {
        while (1 > 0)
        {
            yield return new WaitForSeconds(0.02f);
            if (ConfirmPanel.Instance.IsClickOK)
            {
                QuestManager.Instance.AddAcceptQuestList(this);
                transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform.Find("NoSlotPanel/QuestPanel/Scroll View/Viewport/QuestContent/"), false);   
                break;
            }
            if (ConfirmPanel.Instance.IsClickCancel)
            {
                break;
            }
        }
    }


}
