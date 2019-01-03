using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneStateController 
{
    private ISceneState mState;
    private AsyncOperation mAO;
    private bool mIsRunStart = false;

    public void SetState(ISceneState state, bool isLoadScene = true)
    {
        if (mState != null)
        {
            mState.StateEnd();
        }
        mState = state;
        if (isLoadScene)
        {
            mAO = SceneManager.LoadSceneAsync(mState.SceneName);
            mIsRunStart = false;
        }
        else
        {
            mState.StateStart();
            mIsRunStart = true;
        }
    }

    public void StateUpdate()
    {
        if (mAO != null && mAO.isDone == false) return;
        if (mAO != null && mAO.isDone == true && mIsRunStart == false)
        {
            mState.StateStart();
            mIsRunStart = true;
        }
        if (mState != null)
        {
            mState.StateUpDate();
        }
    }

}
