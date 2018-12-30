using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class Bag_PutOnPanel : BasePanel<Bag_PutOnPanel>
{
    private PlayerStatus PS;
    //private static Bag_PutOnPanel _instance;
    //public static Bag_PutOnPanel Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = GameObject.FindGameObjectWithTag("Canvas").transform.Find("InventoryPanel/Bag_PutOnPanel").GetComponent<Bag_PutOnPanel>();
    //        }
    //        return _instance;
    //    }
    //}
    public Color ShowingColor;
    public Color HidingColor;
    public Text CoinCountText;

    public override void Start()
    {
        base.Start();
        Button equipmentBtn = UITool.GetButton(gameObject, "EquipmentBtn");
        Button consumableBtn = UITool.GetButton(gameObject, "ConsumableBtn");
        Button materialsBtn = UITool.GetButton(gameObject, "MaterialsBtn");
        Button OtherItemBtn = UITool.GetButton(gameObject, "OtherItemBtn");
        CoinCountText = UITool.FindChild<Text>(gameObject, "CoinCount");

        Image equipmentButtonLabel = UITool.FindChild(gameObject, "EquipmentBtn").GetComponent<Image>();
        Image consumableButtonLabel = UITool.FindChild(gameObject, "ConsumableBtn").GetComponent<Image>();
        Image materialsButtonLabel = UITool.FindChild(gameObject, "MaterialsBtn").GetComponent<Image>();
        Image otheritemButtonLabel = UITool.FindChild(gameObject, "OtherItemBtn").GetComponent<Image>();

        PS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        ShowingColor = Color.black;
        HidingColor = Color.white;
        equipmentButtonLabel.color = ShowingColor;
        
        equipmentBtn.onClick.AddListener(() =>
        {
            EquipmentPanel.Instance.Show();
            ConsumablePanel.Instance.Hide();
            MaterialsPanel.Instance.Hide();
            OtherItemPanel.Instance.Hide();
            equipmentButtonLabel.color = ShowingColor;
            consumableButtonLabel.color = HidingColor;
            materialsButtonLabel.color = HidingColor;
            otheritemButtonLabel.color = HidingColor;

        }

        );
        consumableBtn.onClick.AddListener(() =>
        {
            EquipmentPanel.Instance.Hide();
            ConsumablePanel.Instance.Show();
            MaterialsPanel.Instance.Hide();
            OtherItemPanel.Instance.Hide();
            equipmentButtonLabel.color = HidingColor;
            consumableButtonLabel.color = ShowingColor;
            materialsButtonLabel.color = HidingColor;
            otheritemButtonLabel.color = HidingColor;
        });
        materialsBtn.onClick.AddListener(() =>
        {
            EquipmentPanel.Instance.Hide();
            ConsumablePanel.Instance.Hide();
            MaterialsPanel.Instance.Show();
            OtherItemPanel.Instance.Hide();
            equipmentButtonLabel.color = HidingColor;
            consumableButtonLabel.color = HidingColor;
            materialsButtonLabel.color = ShowingColor;
            otheritemButtonLabel.color = HidingColor;
        });
        OtherItemBtn.onClick.AddListener(() =>
        {
            EquipmentPanel.Instance.Hide();
            ConsumablePanel.Instance.Hide();
            MaterialsPanel.Instance.Hide();
            OtherItemPanel.Instance.Show();
            equipmentButtonLabel.color = HidingColor;
            consumableButtonLabel.color = HidingColor;
            materialsButtonLabel.color = HidingColor;
            otheritemButtonLabel.color = ShowingColor;
        });
        UpdateCoin();
    }


    public void UpdateCoin()
    {
        CoinCountText.text = PS.CoinCount.ToString();
    }


    public override void Show()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        Tweener tweener = transform.DOMoveX(700, 0.3f);
        tweener.SetEase(Ease.InOutExpo);
        var top_idx = gameObject.transform.parent.childCount - 1;
        gameObject.transform.SetSiblingIndex(top_idx); // 放到顶层
    }
}
