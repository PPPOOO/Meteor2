using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FSMSystem
{
    private List<FSMState> mStates = new List<FSMState>();
    private FSMState mCurrentState;
    public FSMState currentState { get { return mCurrentState; } }

    public void AddState(params FSMState[] states)
    {
        foreach (FSMState s in states)
        {
            AddState(s);
        }
    }
    public void AddState(FSMState state)
    {
        if (state == null)
        {
            Debug.LogError("要添加的状态为空"); return;
        }
        if (mStates.Count == 0)
        {
            mStates.Add(state);
            mCurrentState = state;
            mCurrentState.DoBeforeEntering();
            return;
        }
        foreach (FSMState s in mStates)
        {
            if (s.stateID == state.stateID)
            {
                Debug.LogError("已存在"); return;
            }
        }
        mStates.Add(state);
    }
    public void DeleteState(StateID stateID)
    {
        if (stateID == StateID.NullState)
        {
            Debug.LogError("要删除的ID为空"); return;
        }
        foreach (FSMState s in mStates)
        {
            if (s.stateID == stateID)
            {
                mStates.Remove(s); return;
            }
        }
        Debug.LogError("要删除的StateID不存在于集合中");
    }
    public void PerformTransition(Transition trans)
    {
        if (trans == Transition.NullTransition)
        {
            Debug.LogError("要执行的转换条件为空"); return;
        }
        StateID nextStateID = mCurrentState.GetOutPutState(trans);
        if (nextStateID == StateID.NullState)
        {
            Debug.LogError("没有对应可转换状态"); return;
        }
        foreach (FSMState s in mStates)
        {
            if (s.stateID == nextStateID)
            {
                mCurrentState.DoBeforeLeaving();
                mCurrentState = s;
                mCurrentState.DoBeforeEntering();
                break;
            }
        }
    }
}
