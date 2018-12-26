using UnityEngine;
using System.Collections;

public class ChaseState : FSMState
{
    public ChaseState(GameObject gameObject, FSMSystem fsm) : base(gameObject, fsm)
    {
        mStateID = StateID.Chase;
    }
    public override void Act()
    {
        mGameObject.transform.position = Vector3.MoveTowards(mGameObject.transform.position, mPlayer.transform.position, moveSpeed * Time.deltaTime);
           
    }

    public override void Reason()
    {
        mDistance = Vector3.Distance(mGameObject.transform.position, mPlayer.transform.position);

        if (mDistance <= 1)
        {
            mFSM.PerformTransition(Transition.CanAttack);
        }
        else if (mDistance >= 10)
        {
            mFSM.PerformTransition(Transition.LostTarget);
        }
    }
}
