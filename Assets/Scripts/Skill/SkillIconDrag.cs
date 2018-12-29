using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SkillIconDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject cloneSkillIcon;
    private GameObject shortcutPanel;
    private SkillUI mSkillUI;
    // begin dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        mSkillUI = MonoBehaviourTool.Instance.GetOverUI().GetComponentInParent<SkillUI>();
        cloneSkillIcon = GameObject.Instantiate(MonoBehaviourTool.Instance.GetOverUI().gameObject);
        Transform CanvasPos = GameObject.FindGameObjectWithTag("Canvas").transform;
        cloneSkillIcon.transform.SetParent(CanvasPos);
        cloneSkillIcon.GetComponent<Image>().raycastTarget = false;

    }

    // during dragging
    public void OnDrag(PointerEventData eventData)
    {
        //SetDraggedPosition(eventData);
        var rt = cloneSkillIcon.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
        }
    }

    // end dragging
    public void OnEndDrag(PointerEventData eventData)
    {
        shortcutPanel = GameObject.FindGameObjectWithTag("Canvas").transform.Find("ResidentPanel/ShortcutPanel").gameObject;
        if (MonoBehaviourTool.Instance.GetOverUI() == null) { Destroy(cloneSkillIcon); }
        else if (MonoBehaviourTool.Instance.GetOverUI().transform.parent.gameObject == shortcutPanel)
        {
            MonoBehaviourTool.Instance.GetOverUI().GetComponent<ShortcutSlot>().SetSkill(mSkillUI.Info.ID);
            Destroy(cloneSkillIcon);
        }
        else
        {
            Destroy(cloneSkillIcon);
        }

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