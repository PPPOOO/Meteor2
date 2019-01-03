using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HUDText : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    private RectTransform r_Transform;
    private Text Des;
    private Canvas canvas;
    Vector3 aa;
    Vector3 bb;

    private void Start()
    {
        aa = target.position;
        
    }

    public void ShowHUDText( string des)
    {
        Des = GetComponent<Text>();
        Des.text = des;
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        r_Transform = GetComponent<RectTransform>();
        Destroy(gameObject, 3f);
        bb = target.position + Vector3.up;
        InvokeRepeating("ShowAnimation", 0, 0.03f);
        
    }

    public void ShowDamgeValue(string des)
    {
        Des = GetComponent<Text>();
        Des.text = des;
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        r_Transform = GetComponent<RectTransform>();
        Destroy(gameObject, 1f);
        bb = target.position + Vector3.up;
        InvokeRepeating("ShowAnimation", 0, 0.03f);
    }
   
    public void ShowAnimation()
    {
        if (target != null)
        {
            aa = Vector3.MoveTowards(aa, bb, 0.03f);
            
            Vector3 GTSPos = RectTransformUtility.WorldToScreenPoint(Camera.main, aa);
            


            Vector2 uisize = canvas.GetComponent<RectTransform>().sizeDelta;//得到画布的尺寸
            Vector2 screenpos = Camera.main.WorldToScreenPoint(aa);//将世界坐标转换为屏幕坐标
            Vector2 screenpos2;
            screenpos2.x = screenpos.x;//转换为以屏幕中心为原点的屏幕坐标
            screenpos2.y = screenpos.y ;
            Vector2 uipos;
            uipos.x = (screenpos2.x / Screen.width) * uisize.x;
            uipos.y = (screenpos2.y / Screen.height) * uisize.y;//得到UGUI的anchoredPosition
            //r_Transform.anchoredPosition3D = GTSPos;
            r_Transform.anchoredPosition3D = uipos;
        }
    }
}