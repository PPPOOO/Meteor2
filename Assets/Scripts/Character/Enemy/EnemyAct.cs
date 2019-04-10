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
        if (EnemyStatus.IsDead== false)
        {
            mFSM.currentState.Reason();
            mFSM.currentState.Act();
        }
        else
        {
            Debug.Log("dead");
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
        if (EnemyStatus.IsDead == true) return;
        AudioSource.PlayClipAtPoint(mTakeDamage, transform.position);
        EnemyStatus.HPRemainChange(attack);
        StartCoroutine(ShowBodyRed());
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
