using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    private static ToolTip _instance;
    public static ToolTip Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag("Canvas").transform.Find("TempPanel/ToolTip").GetComponent<ToolTip>();
            }
            return _instance;
        }
    }

    private Text toolTipText;
    private Text contentText;
    private Canvas canvas;
    private Vector2 offset =new Vector2 (20, -15);

    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        toolTipText = GetComponent<Text>();
        contentText = transform.Find("Content").GetComponent<Text>();
    }


    public void ShowForTimeInMousePosition(string des, float time)
    {
        Show(des);
        transform.position = Input.mousePosition;
        StartCoroutine(WaitForHide(time));
    }


    public void ShowFollowMouse(string des)
    {
        Show(des);
        InvokeRepeating("FollowMouse", 0, 0.02f);
    }
    
    public void ShowFollowMouseWithOffset(string des)
    {
        Show(des);
        InvokeRepeating("FollowMouseWithOffset", 0, 0.02f);
    }


    private void Show(string text)
    {
        gameObject.SetActive(true);
        toolTipText.text = text;
        contentText.text = text;
    }

    private void FollowMouse()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
        transform.localPosition = position;
    }
    private void FollowMouseWithOffset()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
        transform.localPosition = position+offset;
    }
    IEnumerator WaitForHide(float time)
    {
        yield return new WaitForSeconds(time);
        Hide();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    } 

}
