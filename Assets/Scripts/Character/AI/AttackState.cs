using UnityEngine;
using System.Collections;

public class AttackState : FSMState
{
    private int mAttacktTime = 1;
    private float mAttackTimer = 1;
    public AttackState(GameObject gameObject, FSMSystem fsm) : base(gameObject, fsm)
    {
        mStateID = StateID.Attack;
        mAttackTimer = mAttacktTime;
    }
    public override void Act()
    {
        mAttackTimer += Time.deltaTime;
        if (mAttackTimer >= mAttacktTime)
        {
            mPlayer.GetComponent<CharacetStatus>().HPRemainChange(-mGameObject.GetComponent<CharacetStatus>().AD);
            mAttackTimer = 0;
        }
    }

    public override void Reason()
    {
        mDistance = Vector3.Distance(mGameObject.transform.position, mPlayer.transform.position);
        if (mDistance <= 10&&mDistance>=1)
        {
            mFSM.PerformTransition(Transition.SeeTarget);
        }
        else if (mDistance >= 10)
        {
            mFSM.PerformTransition(Transition.LostTarget);
        }
    }

}
