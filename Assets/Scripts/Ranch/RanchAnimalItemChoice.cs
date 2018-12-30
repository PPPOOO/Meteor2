using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RanchAnimalItemChoice :MonoBehaviour,IPointerDownHandler
{
    private int ID;
    private ItemPet ItemPet;
    private Image Icon;
    private Text Name;
    private PlayerStatus mPlayerStatus;



    public void SetAnimal(int id)
    {
        Icon = UITool.FindChild<Image>(gameObject, "Icon");
        Name = UITool.FindChild<Text>(gameObject, "Name");
        mPlayerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();

        ID = id;
        ItemPet = InventoryManager.Instance.GetItemById(ID) as ItemPet;
        Icon.sprite = Resources.Load<Sprite>(ItemPet.Sprite);
        Name.text = ItemPet.Name;
    }





    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OtherItemPanel.Instance.ReduceItem(ID);
        ChoiceRanchAnimalPanel.Instance.Hide();
        RanchnManager.Instance.CreatAnimalInRanch(ItemPet.PetID);
    }

}
