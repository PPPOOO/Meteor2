using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAct : MonoBehaviour
{
    private FSMSystem mFSM;
    public EnemyStatus EnemyStatus;
    private SpriteRenderer mSR;
    private Color mNormalColor;
    public AudioClip mTakeDamage;
    private bool mIsDead = false;
    private PlayerStatus mPS;

    public void Start()
    {
        MakeFSM();
        mPS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        EnemyStatus = GetComponent<EnemyStatus>();
        mSR = GetComponent<SpriteRenderer>();
        mNormalColor = mSR.material.color;
    }

    public void Update()
    {
        if (mIsDead == false)
        {
            mFSM.currentState.Reason();
            mFSM.currentState.Act();
        }
    }
    private void MakeFSM()
    {
        int i = Random.Range(0, 3);
        
        mFSM = new FSMSystem();
        if (i == 0)
        {
            PatrolState patrol = new PatrolState(gameObject, mFSM);
            patrol.AddTransition(Transition.CanAttack, StateID.Attack);
            patrol.AddTransition(Transition.SeeTarget, StateID.Chase);

            ChaseState chase = new ChaseState(gameObject, mFSM);
            chase.AddTransition(Transition.CanAttack, StateID.Attack);
            chase.AddTransition(Transition.LostTarget, StateID.Patrol);

            AttackState attack = new AttackState(gameObject, mFSM);
            attack.AddTransition(Transition.LostTarget, StateID.Patrol);
            attack.AddTransition(Transition.SeeTarget, StateID.Chase);

            mFSM.AddState(patrol, chase, attack);
        }
        if (i == 1)
        {
            
            ChaseState chase = new ChaseState(gameObject, mFSM);
            chase.AddTransition(Transition.CanAttack, StateID.Attack);
            chase.AddTransition(Transition.LostTarget, StateID.Patrol);

            AttackState attack = new AttackState(gameObject, mFSM);
            attack.AddTransition(Transition.LostTarget, StateID.Patrol);
            attack.AddTransition(Transition.SeeTarget, StateID.Chase);

            mFSM.AddState(chase, attack);
        }
        if (i == 2)
        {
            PatrolState patrol = new PatrolState(gameObject, mFSM);
            patrol.AddTransition(Transition.CanAttack, StateID.Attack);
            patrol.AddTransition(Transition.SeeTarget, StateID.Chase);
            AttackState attack = new AttackState(gameObject, mFSM);
            attack.AddTransition(Transition.LostTarget, StateID.Patrol);
            attack.AddTransition(Transition.SeeTarget, StateID.Chase);
            mFSM.AddState(patrol,attack);
        }
    }

    public void TakeDamage(int attack)
    {
        if (mIsDead == true) return;
        AudioSource.PlayClipAtPoint(mTakeDamage, transform.position);
        EnemyStatus.HPRemainChange(-attack);
        StartCoroutine(ShowBodyRed());
        if (EnemyStatus.HP_Remain <= 0)
        {
            mPS.GetExp(EnemyStatus.enemy.Exp);
            mIsDead = true;
            EnemyStatus.animator.SetBool("IsDead", true);
            QuestManager.Instance.EnemyKilled(EnemyStatus);
            Destroy(gameObject, 1f);
        }
    }

    IEnumerator ShowBodyRed()
    {
        mSR.material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        mSR.material.color = mNormalColor;
    }


    private void OnMouseEnter()
    {
        CursorManager.Instance.SetAttack();

    }

    private void OnMouseExit()
    {
        CursorManager.Instance.SetNormal();
    }
}
