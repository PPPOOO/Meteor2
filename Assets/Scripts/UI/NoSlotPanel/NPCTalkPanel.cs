using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NPCTalkPanel : BasePanel<NPCTalkPanel>
{
    public Button FinishQuest;
    public Button DoQuest;
    public Button AcceptQuest;
    public Button CancelQuest;
    public Button Business;
    public Button Talk;
    public Button Quit;

    public bool IsClickFinshQuest = false;
    public bool IsClickDoquest = false;
    public bool IsClickAcceptQuest = false;
    public bool IsClickCancelQuest = false;
    public bool IsClickBusiness = false;
    public bool IsClickTalk = false;
    public bool IsClickQuit = false;



    public override void Start()
    {
        base.Start();
        FinishQuest.gameObject.SetActive(false);
        DoQuest.gameObject.SetActive(false);
        AcceptQuest.gameObject.SetActive(false);
        CancelQuest.gameObject.SetActive(false);
        Business.gameObject.SetActive(false);
    }

    public override void Show()
    {

        base.Show();
        IsClickFinshQuest = false;
        IsClickDoquest = false;
        IsClickAcceptQuest = false;
        IsClickCancelQuest = false;
        IsClickBusiness = false;
         IsClickTalk = false;
         IsClickQuit = false;
    }

    public override void Hide()
    {
        base.Hide();
        FinishQuest.gameObject.SetActive(false);
        DoQuest.gameObject.SetActive(false);
        AcceptQuest.gameObject.SetActive(false);
        CancelQuest.gameObject.SetActive(false);
        Business.gameObject.SetActive(false);
    }

    public void OnClickFinishQuest()
    {
        IsClickFinshQuest = true;
        Hide();
    }

    public void OnClickDoQuest()
    {
        IsClickDoquest = true;
        Hide();
    }
    public void OnClickAcceptQuest()
    {
        IsClickAcceptQuest = true;
        Hide();
    }
    public void OnClickCancelQuest()
    {
        IsClickCancelQuest = true;
        Hide();
    }
    public void OnClikcBusiness()
    {
        IsClickBusiness = true;
        Hide();
    }
    public void OnClikcTalk ()
    {
        IsClickTalk = true;
        //Hide();
    }
    public void OnClikcQuit()
    {
        IsClickQuit = true;
        Hide();
    }

    
    
}
