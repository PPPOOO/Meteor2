using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GoodsChestPanel : BaseInventoryPanel {

    private static GoodsChestPanel _instance;
    public static GoodsChestPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag("Canvas").transform.Find("BusinessPanel/GoodsChestPanel").GetComponent<GoodsChestPanel>();
            }
            return _instance;
        }
    }


    public override void Show()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        Tweener tweener = transform.DOMoveX(200, 0.3f);
        tweener.SetEase(Ease.InOutExpo);
        var top_idx = gameObject.transform.parent.childCount - 1;
        gameObject.transform.SetSiblingIndex(top_idx); // 放到顶层
    }

    public List<ItemUI> FindGoodsItem()
    {
        List<ItemUI> goodsitemUIs = new List<ItemUI>();
        foreach (Slot goodschestslot in slotList)
        {
            if (goodschestslot.transform.childCount > 0)
            {
                ItemUI goodsItemUI = goodschestslot.transform.GetChild(0).GetComponent<ItemUI>();
                goodsitemUIs.Add(goodsItemUI);
            }
        }
        return goodsitemUIs;
    }
}
