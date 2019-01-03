using UnityEngine;
using System.Collections;

public class PlayerAct : MonoBehaviour
{
    private PlayerMove PM;
    private PlayerStatus PS;
    private Transform Target_normalattack;
    public bool IsNormalAttack=false;
    public GameObject EffectPrefab;
    public Animator animator;

    public bool IsWeaponAttack = false;
    public GameObject WeaponGoPrefab;
    public float WeaponTimer = 0;
    private Transform WeaponDownPos;
    private Transform WeaponUpPos;
    private Transform WeaponRightPos;
    private Transform WeaponLeftPos;

    private void Start()
    {
        animator = GetComponent<Animator>();
        PM = GetComponent<PlayerMove>();
        PS = GetComponent<PlayerStatus>();
        GetWeaponInfo();
    }


    private void Update()
    {
        
        WeaponTimer += Time.deltaTime;
        if(WeaponTimer>=1/PS.AttackRate&& Input.GetKey(KeyCode.F))
        {
            WeaponAttack();
        }
        if (Input.GetMouseButtonDown(1))
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.tag == "Enemy")
            {
                Target_normalattack = hit.collider.transform;
                IsNormalAttack = true;
            }
            else
            {
                Target_normalattack = null;
                IsNormalAttack = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S))
        {
            IsNormalAttack = false;
        }
        
        if (IsNormalAttack==true)
        {
            if (Target_normalattack == null) return;
            float distance = Vector3.Distance(transform.position, Target_normalattack.position);
            
            if (distance <= PS.AttackDistance)
            {
                PS.AttackTimer += Time.deltaTime;
                if (PS.AttackTimer >= (1f / PS.AttackRate))
                {
                    Target_normalattack.gameObject.GetComponent<EnemyAct>().TakeDamage(-PS.AD);
                    Instantiate(EffectPrefab, Target_normalattack.position, Quaternion.identity);
                    PS.AttackTimer = 0;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, Target_normalattack.position, PS.MoveSpeed * Time.deltaTime);
            }
        }
    }

    private void GetWeaponInfo()
    {
        WeaponGoPrefab = Resources.Load<GameObject>("Weapon");
        WeaponDownPos = UITool.FindChild<Transform>(gameObject, "Down");
        WeaponUpPos = UITool.FindChild<Transform>(gameObject, "Up");
        WeaponRightPos = UITool.FindChild<Transform>(gameObject, "Right");
        WeaponLeftPos = UITool.FindChild<Transform>(gameObject, "Left");
    }

    private void WeaponAttack()
    {
        GameObject go = null;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Down"))
        {
            go = Instantiate(WeaponGoPrefab, WeaponDownPos);
            go.GetComponent<WeaponAttack>().weaponDir = global::WeaponAttack.WeaponDir.Down;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Up"))
        {
            go = Instantiate(WeaponGoPrefab, WeaponUpPos);
            go.GetComponent<WeaponAttack>().weaponDir = global::WeaponAttack.WeaponDir.Up;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Right"))
        {
            go = Instantiate(WeaponGoPrefab, WeaponRightPos);
            go.GetComponent<WeaponAttack>().weaponDir = global::WeaponAttack.WeaponDir.Right;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Left"))
        {
            go = Instantiate(WeaponGoPrefab, WeaponLeftPos);
            go.GetComponent<WeaponAttack>().weaponDir = global::WeaponAttack.WeaponDir.Left;
        }
        go.transform.localPosition = Vector3.zero;
        WeaponTimer = 0;
    }
    public void UseSkill(SkillBaseInfo skillBaseInfo)
    {
        switch (skillBaseInfo.Type)
        {
            case SkillBaseInfo.ReleaseType.Self:
                SkillSelf skillSelf = skillBaseInfo as SkillSelf;
                UseSelfSkill(skillSelf);
                break;
            case SkillBaseInfo.ReleaseType.SelfRange:
                SkillSelfRange skillSelfRange = skillBaseInfo as SkillSelfRange;
                UseSelfRangeSkill(skillSelfRange);
                break;
            case SkillBaseInfo.ReleaseType.Multi:
                SkillMulti skillMulti = skillBaseInfo as SkillMulti;
                UseMultiSkill(skillMulti);
                break;
            case SkillBaseInfo.ReleaseType.Single:
                SkillSingle skillSingle = skillBaseInfo as SkillSingle;
                UseSingleSkill(skillSingle);
                break;
            case SkillBaseInfo.ReleaseType.Trajectory:
                SkillTrajectory skillTrajectory = skillBaseInfo as SkillTrajectory;
                UseTraSkill(skillTrajectory);
                break;
        }
    }

    public void UseMultiSkill(SkillMulti skillMulti)
    {
        Vector3 targetpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetpos.z = transform.position.z;

        float distance = Vector3.Distance(gameObject.transform.position, targetpos);
        if (distance <= skillMulti.Distance)
        {
            GameObject SkillSensePrefab = Resources.Load<GameObject>("Skill/SkillSenseMulti");
            GameObject effPrefab = Resources.Load<GameObject>("Effect/Boom/Boom");
            GameObject SkillSensego1 = Instantiate(SkillSensePrefab);
            GameObject eff1 = Instantiate(effPrefab);
            SkillSensego1.transform.position = targetpos;
            SkillSensego1.GetComponent<CircleCollider2D>().radius = skillMulti.Range;
            SkillSensego1.GetComponent<SkillSenseMulit>().JudgeObejct(gameObject, skillMulti);
            eff1.transform.position = targetpos;
        }

    }

    public void UseTraSkill(SkillTrajectory skillTrajectory)
    {
        Vector3 targetpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetpos.z = transform.position.z;
        GameObject SkillSenseTraPrefab = Resources.Load<GameObject>("Skill/SkillSenseTra");
        GameObject SkillSenseTra1 = Instantiate(SkillSenseTraPrefab);
        SkillSenseTra1.transform.position = gameObject.transform.position;
        SkillSenseTra1.GetComponent<CircleCollider2D>().radius = skillTrajectory.ShotSize;
        SkillSenseTra1.GetComponent<DestroyForTime>().time = skillTrajectory.ShotTime;
        SkillSenseTra1.transform.rotation = Quaternion.Euler(Vector3.zero);
        Vector3 chazhi = targetpos - transform.position;
        float angle = (Mathf.Atan2(chazhi.x, chazhi.y) * Mathf.Rad2Deg) / 2;
        angle = 180 - angle;
        SkillSenseTra1.transform.Rotate(Vector3.forward, angle);
        SkillSenseTra1.GetComponent<SkillSenseTra>().mSpeed = skillTrajectory.ShotSpeed;
        SkillSenseTra1.GetComponent<SkillSenseTra>().JudgeObejct(gameObject, skillTrajectory);
    }

    public void UseSelfSkill(SkillSelf skillSelf)
    {
        SkillManager.Instance.CaleSkillAttrObjectValue(gameObject, gameObject, skillSelf);
    }

    public void UseSelfRangeSkill(SkillSelfRange skillSelfRange)
    {
        GameObject SkillSensePrefab = Resources.Load<GameObject>("Skill/SkillSenseMulti");
        GameObject effPrefab = Resources.Load<GameObject>("Effect/Boom/Boom");
        GameObject SkillSensego1 = Instantiate(SkillSensePrefab);
        GameObject eff1 = Instantiate(effPrefab);
        SkillSensego1.GetComponent<CircleCollider2D>().radius = skillSelfRange.Range;
        SkillSensego1.GetComponent<SkillSenseMulit>().JudgeObejct(gameObject, skillSelfRange);
        SkillSensego1.transform.position = gameObject.transform.position;
        eff1.transform.position = gameObject.transform.position;
    }


    public void UseSingleSkill(SkillSingle skillSingle)
    {
        
        Vector3 targetpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetpos.z = transform.position.z;

        float distance = Vector3.Distance(gameObject.transform.position, targetpos);
        if (distance <= skillSingle.Distance)
        {
            RaycastHit2D myRay = Physics2D.Raycast(targetpos, Vector2.zero);
            if (myRay.collider!=null)
            {
                SkillManager.Instance.CaleSkillAttrObjectValue(gameObject, myRay.collider.gameObject, skillSingle);
            }
        }
    }

    

}
