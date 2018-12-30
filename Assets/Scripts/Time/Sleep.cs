using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class Sleep : MonoBehaviour
{
    private Image Bg;
    private PlayerStatus mPlayerStatus;
    private void Start()
    {
        mPlayerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        Bg = GameObject.FindGameObjectWithTag("Canvas").transform.Find("GoToBlack").GetComponent<Image>();

    }

    private void OnMouseDown()
    {
        if (MonoBehaviourTool.Instance.GetOverUI() == true) return;
        float distance = Vector3.Distance(mPlayerStatus.transform.position, gameObject.transform.position);
        if (distance < 2)
        {
            ConfirmPanel.Instance.Show();
            StartCoroutine(SleepConfirm());
        }
    }

    IEnumerator SleepConfirm()
    {
        while (1 > 0)
        {
            yield return new WaitForSeconds(0.05f);
            if (ConfirmPanel.Instance.IsClickOK)
            {
                StartCoroutine( Sleeping());
                break;
            }
            if (ConfirmPanel.Instance.IsClickCancel)
            {
                break;
            }
        }
    }

    IEnumerator Sleeping()
    {
        Bg.DOFade(1, 0.5f);
        yield return new WaitForSeconds(1f);
        
        TimeManager.Instance.NewDay();
        mPlayerStatus.HPRemainChange(mPlayerStatus.HP);
        mPlayerStatus.MPRemainChange(mPlayerStatus.MP);
        mPlayerStatus.EPRemainChange(mPlayerStatus.EP);
        Bg.DOFade(0, 0.5f);
    }
}
