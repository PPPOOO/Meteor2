using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusinessManager : MonoBehaviour {

    private static BusinessManager _instance;
    public static BusinessManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag("Manager").GetComponent<BusinessManager>();
            }
            return _instance;
        }
    }

    public List<GoodsItem> goods = new List<GoodsItem>();
    private GameObject player;
    public GameObject GoodsChoiceItem;
    public int PrayTimes = 0;
    public int PolishTimes = 0;
    public int PrayEP = 0;
    public int PolishEP = 0;
    private List<ItemUI> GoodsItemUIs;
    public Transform GoodsShelftransform;
    private Transform mContent;
    public int mSellGoodsDayCount=3;

    private void Start()
    {
        mContent = GameObject.FindGameObjectWithTag("Canvas").transform.Find("BusinessPanel/ChoiceGoodsPanel/Scroll View/Viewport/Content");
        player = GameObject.FindGameObjectWithTag("Player");
        GameEventManager.Instance.RegisterObserver(GameEventType.NewDay, new NewDayObserverBusinessManager(this));
    }



    private void Update()
    {
        CheckDistance();
    }


    public int CalcPrayEP()
    {
        PrayEP = goods.Count * 5* (PrayTimes+1);
        return PrayEP;
    }

    public int CalcPolishEP()
    {
        PolishEP = goods.Count * 10*(PolishTimes+1);
        return PolishEP;
    }



    public void Pray()
    {
        
        if (player.GetComponent<PlayerStatus>().TakeEP(PrayEP))
        {
            for (int i =goods.Count-1 ; i >=0; i--)
            {
                goods[i].Pray(0.1f);
            }

            PrayTimes++;
            MSBusinessPanel.Instance.UpdateEP();
            ToolTip.Instance.ShowForTimeInMousePosition("祈祷成功！！", 2);
            ToolTip.Instance.transform.position = Input.mousePosition;
        }
        else
        {
            ToolTip.Instance.ShowForTimeInMousePosition("体力不够！！", 2);
            ToolTip.Instance.transform.position = Input.mousePosition;
        }
        
    }

    public void Polish()
    {
        if (player.GetComponent<PlayerStatus>().TakeEP(PolishEP))
        {
            for (int i = goods.Count - 1; i >= 0; i--)
            {
                goods[i].Polish(20);
            }
            PolishTimes++;
            MSBusinessPanel.Instance.UpdateEP();
            ToolTip.Instance.ShowForTimeInMousePosition("打磨成功！！", 2);
            ToolTip.Instance.transform.position = Input.mousePosition;
        }
        else
        {
            ToolTip.Instance.ShowForTimeInMousePosition("体力不够！！", 2);
            ToolTip.Instance.transform.position = Input.mousePosition;
        }
    }


    public void Business()
    {
        if (goods.Count == 0)
        {
            ToolTip.Instance.ShowForTimeInMousePosition("没摆东西卖空气呢？",2);
            ToolTip.Instance.transform.position = Input.mousePosition;
        }
        else if (goods.Count <= mSellGoodsDayCount)
        {
            StartCoroutine(IEBusiness(goods));
            
        }

    }

    IEnumerator IEBusiness(List<GoodsItem> goodsItems)
    {
        for (int i = goodsItems.Count-1; i >=0; i--)
        {
            goodsItems[i].Business();
            yield return new WaitForSeconds(1f);
        }

    }

    public void CheckDistance()
    {
        if (GoodsShelftransform == null) return;
        float distance = Vector3.Distance(player.transform.position, GoodsShelftransform.position);
        if (distance > 5)
        {
            ChoiceGoodsPanel.Instance.Hide();
        }
    }



    public void AddGoods(GoodsItem goodsitem)
    {
        goods.Add(goodsitem);
        CalcPrayEP();
        CalcPolishEP();
    }
    public void RemoveGoods(GoodsItem goodsitem)
    {
        goods.Remove(goodsitem);
        CalcPrayEP();
        CalcPolishEP();
    }




    public void NewDayCome()
    {
        PrayTimes = 0;
        PolishTimes = 0;
        MSBusinessPanel.Instance.UpdateEP();
        foreach(GoodsItem item in goods)
        {
            item.Probability = 0.5f;
            item.SellPrice = item.Goods.SellPrice;
        }
    }


    public void CreatChoiceGoods()
    {
        GoodsItemUIs = GoodsChestPanel.Instance.FindGoodsItem();
        if (GoodsItemUIs == null) return;
        foreach (ItemUI itemUI in GoodsItemUIs)
        {
            GameObject GoodsItem = Instantiate(GoodsChoiceItem, mContent, false);
            GoodsItem.GetComponent<GoodsChoiceItem>().SetGoodsItem(itemUI.Item.ID, itemUI.Amount);
        }
    }


}
