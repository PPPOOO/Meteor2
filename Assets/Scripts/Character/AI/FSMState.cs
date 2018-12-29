using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Transition
{
    NullTransition = 0,
    SeeTarget,
    CanAttack,
    LostTarget
}
public enum StateID
{
    NullState,
    Idle,
    Patrol,
    Chase,
    Attack
}


public abstract class FSMState
{

    protected Dictionary<Transition, StateID> mMap = new Dictionary<Transition, StateID>();
    protected StateID mStateID;
    public StateID stateID { get { return mStateID; } }
    protected GameObject mGameObject;
    protected FSMSystem mFSM;
    protected float moveSpeed;
    protected GameObject mPlayer;
    protected float mDistance;

    
    public FSMState(GameObject gameObject,FSMSystem fsm)
    {
        mFSM=fsm;
        mGameObject = gameObject;
        mPlayer = GameObject.FindGameObjectWithTag("Player");
    }


    public void AddTransition(Transition trans, StateID id)
    {
        if (trans == Transition.NullTransition)
        {
            Debug.LogError("State Error:trans不能为空"); return;
        }
        if (id == StateID.NullState)
        {
            Debug.LogError("State Error:id不能为空"); return;
        }
        if (mMap.ContainsKey(trans))
        {
            Debug.LogError("State Error:" + trans + "已经添加上了"); return;
        }
        mMap.Add(trans, id);
    }

    public void DeleteTransition(Transition trans)
    {
        if (mMap.ContainsKey(trans) == false)
        {
            Debug.LogError("删除转换条件的时候，转换条件：[" + trans + "]不存在"); return;

        }
        mMap.Remove(trans);
    }

    public StateID GetOutPutState(Transition trans)
    {
        if (mMap.ContainsKey(trans) == false)
        {
            return StateID.NullState;
        }
        else
        {
            return mMap[trans];
        }
    }


    public virtual void DoBeforeEntering() { }
    public virtual void DoBeforeLeaving() { }
    public abstract void Reason();
    public abstract void Act();

}
