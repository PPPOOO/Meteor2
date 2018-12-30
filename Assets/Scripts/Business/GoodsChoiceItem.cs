using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GoodsChoiceItem : MonoBehaviour, IPointerDownHandler
{
    private int ID;
    private int Count;
    private Item goodsitem;
    private Image Icon;
    private Text Name;
    private Text CountText = null;
    private Text SellPrice;
    private PlayerStatus mPlayerStatus;



    public void SetGoodsItem(int id, int count)
    {
        Icon = UITool.FindChild<Image>(gameObject, "Icon");
        Name = UITool.FindChild<Text>(gameObject, "Name");
        CountText = UITool.FindChild<Text>(gameObject, "Count");
        SellPrice = UITool.FindChild<Text>(gameObject, "SellPrice");
        mPlayerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();

        ID = id;
        goodsitem = InventoryManager.Instance.GetItemById(ID);
        Icon.sprite = Resources.Load<Sprite>(goodsitem.Sprite);
        Name.text = goodsitem.Name;
        SellPrice.text ="售价"+ goodsitem.SellPrice.ToString() ;
        if (goodsitem.Capacity == 0)
        {
            CountText.text = " ";
        }
        else
        {
            CountText.text = count.ToString();
        }
        
    }





    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (mPlayerStatus.TakeEP(5))
        {
            GoodsChestPanel.Instance.ReduceItem(ID);
            ChoiceGoodsPanel.Instance.Hide();
            BusinessManager.Instance.GoodsShelftransform.SendMessage("ShowGoodsItem", ID);
        }
        else
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>(), Input.mousePosition, null, out position);
            ToolTip.Instance.ShowForTimeInMousePosition("体力不够！！",2);
        }
    }



}
