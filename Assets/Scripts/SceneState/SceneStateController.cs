using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneStateController :MonoBehaviour
{
    private ISceneState mState;
    private AsyncOperation mAO;
    private bool mIsRunStart = false;

    private SceneStateController controller = null;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        controller = new SceneStateController();
        SetState(new StartState(controller), false);
    }

    public void Update()
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

}
