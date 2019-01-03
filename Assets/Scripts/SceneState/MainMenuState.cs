using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuState : ISceneState
{
    public MainMenuState(SceneStateController controller) : base("02MainMenuScene", controller)
    {

    }


    public override void StateStart()
    {
        GameObject.Find("StartBtn").GetComponent<Button>().onClick.AddListener(OnStartButtonClick);

    }
    public void OnStartButtonClick()
    {
        mController.SetState(new MainState(mController));
    }
}
