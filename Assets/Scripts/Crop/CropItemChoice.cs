using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CropItemChoice : MonoBehaviour,IPointerDownHandler,IPointerExitHandler
{
    private int ID;
    private int Count;
    private ItemSeed Seed;
    private Image Icon;
    private Text Name;
    private Text CountText = null;
    private PlayerStatus mPlayerStatus;



    public void SetSeed(int id,int count)
    {
        Icon = UITool.FindChild<Image>(gameObject, "Icon");
        Name = UITool.FindChild<Text>(gameObject, "Name");
        CountText = UITool.FindChild<Text>(gameObject, "Count");
        mPlayerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        
        ID = id;
        Seed = InventoryManager.Instance.GetItemById(ID) as ItemSeed;
        Icon.sprite = Resources.Load<Sprite>(Seed.Sprite);
        CountText.text = count.ToString();
        Name.text = Seed.Name;
    }





    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(mPlayerStatus.TakeEP(5))
        {
            OtherItemPanel.Instance.ReduceItem(ID);
            ChoiceCropPanel.Instance.Hide();
            CropManager.Instance.Croplandtransform.SendMessage("Plant", ID);
        }
        else
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>(), Input.mousePosition, null, out position);
            ToolTip.Instance.ShowForTimeInMousePosition("体力不够！！",2);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTip.Instance.Hide();
    }
}
