using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartState : ISceneState
{
    public StartState(SceneStateController controller) : base("01StartScene", controller)
    {

    }

    private Image mLogo;
    private float mSmoothingTime = 1;

    private float mWaitTime = 0;

    public override void StateStart()
    {
        mLogo = GameObject.Find("Logo").GetComponent<Image>();
        mLogo.color = Color.black;
    }

    public override void StateUpDate()
    {
        mLogo.color = Color.Lerp(mLogo.color, Color.white, mSmoothingTime * Time.deltaTime);
        mWaitTime -= Time.deltaTime;
        if (mWaitTime <= 0)
        {
            mController.SetState(new MainMenuState(mController));
        }
    }
}
