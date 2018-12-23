using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PickedItem : MonoBehaviour
{
    private static PickedItem _instance;
    public static PickedItem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag("Canvas").transform.Find("TempPanel/PickedItem").GetComponent<PickedItem>();
            }
            return _instance;
        }
    }
    
    public bool IsPickedItem=false;
    public Image sprite;
    public Text Amount;
    public int ID;
    public int Count;
    public Item item;
    private Canvas canvas;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        Hide();
    }

    public void SetPickItem(int id,int count = 1)
    {
        gameObject.SetActive(true);
        IsPickedItem = true;
        ID = id;
        Count = count;
        item = InventoryManager.Instance.GetItemById(ID);
        sprite.sprite = Resources.Load<Sprite>(item.Sprite);
        if (item.Capacity == 1)
        {
            Amount.text = " ";
        }
        else
        {
            Amount.text = Count.ToString();
        }
        InvokeRepeating("FollowMouse", 0, 0.03f);
    }
    

    public void ReduceAmount(int count)
    {
        Count -= count;
        Amount.text = Count.ToString();
    }


    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        IsPickedItem = false;
        gameObject.SetActive(false);
    }
    private void FollowMouse()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
        transform.localPosition = position;
    }
}
