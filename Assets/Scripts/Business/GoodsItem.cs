using UnityEngine;
using System.Collections;

public class GoodsItem : MonoBehaviour
{
    public int ID;
    public SpriteRenderer SR;
    public Item Goods;
    public int SellPrice;
    public float Probability=0.5f;
    
    public void SetID(int id)
    {
        SR = GetComponent<SpriteRenderer>();
        ID = id;

        BusinessManager.Instance.AddGoods(this);
        Goods = InventoryManager.Instance.GetItemById(ID) ;
        SR.sprite = Resources.Load<Sprite>(Goods.Sprite);
        SellPrice = Goods.SellPrice;
    }

    public void Pray(float value)
    {
        Probability += value ;
    }

    public void Polish(float value)
    {
        SellPrice +=(int) value ;
    }

    public void Business()
    {
        int number = Random.Range(0, 100);
        if (number <= Probability*100)
        {
            ToolTip.Instance.ShowForTimeInMousePosition(string.Format("<size=30>恭喜你卖出了{0}，\n并获得了{1}金钱</size>", Goods.Name,SellPrice), 2);
            ToolTip.Instance.transform.position = Input.mousePosition;
            BusinessManager.Instance.RemoveGoods(this);
            Destroy(gameObject);
        }
        else
        {
            ToolTip.Instance.ShowForTimeInMousePosition(string.Format("<size=30>有人看上了{0}，\n但是却买不起</size>",Goods.Name),2);
            
        }
    }
    

}


