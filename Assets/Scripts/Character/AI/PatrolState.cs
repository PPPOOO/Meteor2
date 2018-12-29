using UnityEngine;
using System.Collections;

public class PatrolState : FSMState
{
    public PatrolState(GameObject gameObject,FSMSystem fsm) : base(gameObject,fsm)
    {
        mStateID = StateID.Patrol;
    }

    private float halfSideLength = 3;
    private Vector3[] pos = new Vector3[4];
    private bool isFixBeginPos = false;
    private Vector3 beginPos;
    
    private int i = 0;

    //private int mAttacktTime = 1;
    //private float mAttackTimer = 1;

    public override void Act()
    {
        Move1();
    }

    public override void Reason()
    {
        mDistance = Vector3.Distance(mGameObject.transform.position, mPlayer.transform.position);

        if (mDistance<=1)
        {
            mFSM.PerformTransition(Transition.CanAttack);
        }
        else if (mDistance<=5)
        {

            mFSM.PerformTransition(Transition.SeeTarget);
        }
        
    }

    private void Move1()
    {
        if (isFixBeginPos == false)
        {
            Vector3 beginPos = mGameObject.transform.position;
            pos[0] = new Vector3(beginPos.x - halfSideLength, beginPos.y + halfSideLength, beginPos.z);
            pos[1] = new Vector3(beginPos.x + halfSideLength, beginPos.y + halfSideLength, beginPos.z);
            pos[2] = new Vector3(beginPos.x - halfSideLength, beginPos.y - halfSideLength, beginPos.z);
            pos[3] = new Vector3(beginPos.x + halfSideLength, beginPos.y - halfSideLength, beginPos.z);
            isFixBeginPos = true;
        }
        mGameObject.transform.position = Vector3.MoveTowards(mGameObject.transform.position, pos[i],  mGameObject.GetComponent<CharacetStatus>().MoveSpeed * Time.deltaTime);
        if (mGameObject.transform.position == pos[i])
        {
            i++;
            i %= pos.Length;
        }
    }
}
