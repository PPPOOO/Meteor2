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
            Vector3 worldPoint;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, GTSPos, null, out worldPoint))
            {
                transform.position = worldPoint;
            }
            r_Transform.anchoredPosition3D = worldPoint;
        }
    }
}