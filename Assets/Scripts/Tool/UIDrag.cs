using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject DragUI;
    // begin dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        DragUI = gameObject.transform.parent.gameObject;

    }

    // during dragging
    public void OnDrag(PointerEventData eventData)
    {
        //SetDraggedPosition(eventData);
        var rt = DragUI.GetComponent<RectTransform>();
        Vector3 fix = gameObject.transform.position - rt.position;
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos - fix;
        }
    }

    // end dragging
    public void OnEndDrag(PointerEventData eventData)
    {

    }

    /// <summary>
    /// 获取鼠标停留处UI
    /// </summary>
    /// <param name="canvas"></param>
    /// <returns></returns>


    /// <summary>
    /// set position of the dragged game object
    /// </summary>
    /// <param name="eventData"></param>
    private void SetDraggedPosition(PointerEventData eventData)
    {
        var rt = gameObject.GetComponent<RectTransform>();

        // transform the screen point to world point int rectangle
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
        }
    }
}