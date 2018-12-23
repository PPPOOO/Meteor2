using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShopPanel : BaseInventoryPanel
{
    private static ShopPanel _instance;
    public static ShopPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Canvas/NPCPanel/ShopPanel").GetComponent<ShopPanel>();

            }
            return _instance;
        }
    }
    
    public Text CoinCount;
    public PlayerStatus PS;

    public override void Start()
    {
        base.Start();
        PS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        UpdateCoin();
    }


    public override void Show()
    {
        base.Show();
        for (int i = 0; i < 10; i++)
        {
            int randomnum = Random.Range(0, InventoryManager.Instance.itemList.Count);
            StoreItem(InventoryManager.Instance.itemList[randomnum].ID);
        }
    }

    public override void Hide()
    {
        base.Hide();
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                slot.transform.GetChild(0).GetComponent<ItemUI>().ReduceAmount();
            }
        }
    }

    public void UpdateCoin()
    {
        CoinCount.text = PS.CoinCount.ToString();
    }

}
