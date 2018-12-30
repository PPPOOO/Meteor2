using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConfirmPanel : BasePanel<ConfirmPanel>
{


    public Button OKBtn;
    public Button CancelBtn;
    public Canvas canvas;
    public bool IsClickOK = false;
    public bool IsClickCancel = false;
    public override void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        base.Start();
        Hide();
    }

    public override void Show()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        gameObject.SetActive(true);
        IsClickOK = false;
        IsClickCancel = false;
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
        transform.localPosition = position;

    }

    public void OnClickOKBtn()
    {
        IsClickOK = true;
        Hide();
    }
    public void OnCancelBtn()
    {
        IsClickCancel = true;
        Hide();
    }

    public override void Hide()
    {
        base.Hide();
        gameObject.SetActive(false);
    }

}
