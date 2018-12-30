using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceGoodsPanel : BaseInventoryPanel {

    private static ChoiceGoodsPanel _instance;
    public static ChoiceGoodsPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("BusinessPanel/ChoiceGoodsPanel").GetComponent<ChoiceGoodsPanel>();
                
            }
            return _instance;
        }
    }



    public override void Show()
    {
        if (transform.Find("Scroll View/Viewport/Content").childCount != 0)
        {
            GoodsChoiceItem[] goodsItemChioces;
            goodsItemChioces = transform.Find("Scroll View/Viewport/Content").GetComponentsInChildren<GoodsChoiceItem>();
            foreach (GoodsChoiceItem goodsItemChioce in goodsItemChioces)
            {
                goodsItemChioce.DestroySelf();
            }
        }
        BusinessManager.Instance.CreatChoiceGoods();
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();

    }
}
