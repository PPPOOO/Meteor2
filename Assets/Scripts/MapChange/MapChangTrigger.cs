using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class MapChangTrigger : MonoBehaviour
{
    public Transform NextMap;
    public Transform NextMapPos;
    public Transform CurrentMap;
    public Transform CurrentMapPos;
    private Image Bg;
    public BoxCollider2D BoxCollider2D;

    private void Start()
    {
        BoxCollider2D = GetComponent<BoxCollider2D>();
        Bg = GameObject.FindGameObjectWithTag("Canvas").transform.Find("GoToBlack").GetComponent<Image>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        StartCoroutine(ChangeMap(col));
    }


    IEnumerator ChangeMap(Collider2D col)
    {
        Bg.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        CurrentMap.gameObject.SetActive(false);
        NextMap.gameObject.SetActive(true);
        Bg.DOFade(0, 0.5f);
        col.gameObject.transform.position = NextMapPos.position;
    }

}
