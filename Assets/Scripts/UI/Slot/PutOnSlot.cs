using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PutOnSlot : Slot
{
    public Equipment.EquipType equipType;

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (PickedItem.Instance.IsPickedItem == false && transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();

                Item currentItem = currentItemUI.Item;
                currentItemUI.ReduceAmount(1);
                EquipmentPanel.Instance.StoreItem(currentItem.ID);
                ToolTip.Instance.Hide();
                StartCoroutine(UpdateAttr());
            }
        }
        else if(eventData.button == PointerEventData.InputButton.Left)
        {

            if (transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                Item item = currentItemUI.Item;
                if (PickedItem.Instance.IsPickedItem == true&& IsRightItem(PickedItem.Instance.item))
                {
                    
                    currentItemUI.SetItem(PickedItem.Instance.item);
                    PickedItem.Instance.SetPickItem(item.ID);
                    StartCoroutine(UpdateAttr());
                }
                else if (PickedItem.Instance.IsPickedItem == false)
                {
                    PickedItem.Instance.SetPickItem(item.ID);
                    Destroy(currentItemUI.gameObject);
                }
            }
            else
            {
                if (PickedItem.Instance.IsPickedItem == true && IsRightItem(PickedItem.Instance.item))
                {
                    StoreItem(PickedItem.Instance.item);
                    PickedItem.Instance.Hide();
                    StartCoroutine(UpdateAttr());
                }
            }
        }
    }

    public bool IsRightItem(Item item)
    {
        if (item is Equipment && ((Equipment)item).Equiptype == this.equipType)
        {
            return true;
        }
        return false;
    }

    IEnumerator UpdateAttr()
    {
        yield return new WaitForSeconds(0.1f);
        transform.parent.SendMessage("UpdateProperty");
    }

}
