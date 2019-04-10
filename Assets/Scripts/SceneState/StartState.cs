using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartState : ISceneState
{
    public StartState(SceneStateController controller) : base("01StartScene", controller)
    {

    }

    public override void StateStart()
    {
        GameObject.FindGameObjectWithTag("Canvas").transform.Find("StartBtn").GetComponent<Button>().onClick.AddListener(OnStartButtonClick);
        GameObject.FindGameObjectWithTag("Canvas").transform.Find("ExitBtn").GetComponent<Button>().onClick.AddListener(OnExitBtnClick);
    }
    public void OnStartButtonClick()
    {
        mController.SetState(new MainState(mController));
    }
    public void OnExitBtnClick()
    {
        Application.Quit();
    }
}
