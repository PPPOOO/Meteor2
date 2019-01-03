using UnityEngine;
using System.Collections;

public class ISceneState 
{
    private string mSceneName;
    protected SceneStateController mController;


    public ISceneState(string sceneName, SceneStateController controller)
    {
        mSceneName = sceneName;
        mController = controller;
    }

    public string SceneName
    {
        get { return mSceneName; }
    }


    public virtual void StateStart() { }
    public virtual void StateEnd() { }
    public virtual void StateUpDate() { }

}
