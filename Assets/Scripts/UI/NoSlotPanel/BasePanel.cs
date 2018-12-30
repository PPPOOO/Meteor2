using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BasePanel<T> : MonoBehaviour where T:MonoBehaviour
{

   
    private static T m_instance;

    public static T Instance
    {
        get
        {
            return m_instance;
        }
    }

    protected virtual void Awake()
    {
        m_instance = this as T;
    }

    protected CanvasGroup canvasGroup;



    public virtual void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (gameObject.transform.Find("CloseBtn") != null)
        {
            Button closeBtn = UITool.GetButton(gameObject, "CloseBtn");
            closeBtn.onClick.AddListener(Hide);
        }
        Hide();
    }


    public virtual void Show()
    {
        Tweener tweener = transform.DOMoveX(550, 0.3f);
        tweener.SetEase(Ease.InOutExpo);
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        var top_idx = gameObject.transform.parent.childCount - 3;
        gameObject.transform.SetSiblingIndex(top_idx);
    }
    public virtual void Hide()
    {
        Tweener tweener = transform.DOMoveX(2000, 0.3f);
        tweener.SetEase(Ease.InOutExpo);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }
    public void DisplaySwitch()
    {
        if (canvasGroup.alpha == 0)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}

