using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum ShortcutType
{
    Skill,
    Consumable,
    None
}
public class ShortcutSlot : MonoBehaviour, IPointerDownHandler
{
    public KeyCode mKeyCode;

    private ShortcutType type = ShortcutType.None;

    private Image mIcon;
    private Text mNum;

    private int mID;
    private Consumable mConsumable;
    private SkillBaseInfo mInfo;
    private PlayerStatus mPlayerStatus;
    private PlayerAct mPlayerAct;
    private Image MaskIcon;
    private Text CoolTime;

    void Start()
    {
        mPlayerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        mPlayerAct = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAct>();
        mIcon = UITool.FindChild<Image>(gameObject, "Image");
        mIcon.gameObject.SetActive(false);
        MaskIcon = UITool.FindChild<Image>(gameObject, "Mask");
        MaskIcon.fillAmount = 0;
        CoolTime = UITool.FindChild<Text>(gameObject, "CoolTime");
        CoolTime.enabled = false;

    }

    private void Update()
    {
        if (Input.GetKeyDown(mKeyCode))
        {
            if (type == ShortcutType.Consumable)
            {
                ConsumableUse();
            }
            else if (type == ShortcutType.Skill)
            {
                if (MaskIcon.fillAmount>0) return;
                bool mp = mPlayerStatus.TakeMP(mInfo.MP);
                bool ep = mPlayerStatus.TakeEP(mInfo.EP);
                if (mp && ep)
                {
                    mPlayerAct.UseSkill(mInfo);
                    MaskIcon.fillAmount = 1;
                    CoolTime.enabled = true;
                    CoolTime.text = mInfo.CoolTime + "s";
                    StartCoroutine(StartSkillCD(mInfo));
                }
            }
            else
            {
                return;
            }
        }
    }

    IEnumerator StartSkillCD(SkillBaseInfo skillBaseInfo)
    {
        float curTime = 0;
        while (1>0)
        {
            yield return new WaitForSeconds(0.1f);
            curTime += 0.1f;
            CoolTime.text = (skillBaseInfo.CoolTime - curTime).ToString("F1")+ "s";
            MaskIcon.fillAmount = 1 - (curTime / skillBaseInfo.CoolTime);
            if(curTime>= skillBaseInfo.CoolTime)
            {
                CoolTime.enabled = false;
                break;
            }
        }
    }

    public void SetSkill(int id)
    {
        mID = id;
        mInfo = SkillManager.Instance.GetSkillByID(id);
        mIcon.gameObject.SetActive(true);
        mIcon.sprite = Resources.Load<Sprite>(mInfo.Sprite);
        type = ShortcutType.Skill;

    }
    public void SetConsumable(int id)
    {
        mID = id;
        mConsumable = InventoryManager.Instance.GetItemById(mID) as Consumable;
        mIcon.gameObject.SetActive(true);
        mIcon.sprite = Resources.Load<Sprite>(mConsumable.Sprite);
        type = ShortcutType.Consumable;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (PickedItem.Instance.IsPickedItem==true&&PickedItem.Instance.item.Type==Item.ItemType.Consumable)
        {
            SetConsumable(PickedItem.Instance.item.ID);
            ConsumablePanel.Instance.StoreItem(PickedItem.Instance.item, PickedItem.Instance.Count);
            PickedItem.Instance.Hide();
        }
    }

    public void ConsumableUse()
    {
        bool success = ConsumablePanel.Instance.ReduceItem(mID);
        if (success)
        {
            for(int i=0;i< mConsumable.ApplyAttrEffects.Count; i++)
            {
                AttrManager.Instance.ChangeAttrByType(mPlayerAct.gameObject, mConsumable.ApplyAttrEffects[i]);
            }
            if (ConsumablePanel.Instance.CheckItemCount(InventoryManager.Instance.GetItemById(mID))<=0)
            {
                type = ShortcutType.None;
                mIcon.gameObject.SetActive(false);
            }
            
        }
    }
}
