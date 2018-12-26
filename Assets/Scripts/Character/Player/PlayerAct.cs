using UnityEngine;
using System.Collections;

public class PlayerAct : MonoBehaviour
{
    private PlayerMove PM;
    private PlayerStatus PS;
    private Transform Target_normalattack;
    public bool IsNormalAttack=false;
    public GameObject EffectPrefab;


    private void Start()
    {
        PM = GetComponent<PlayerMove>();
        PS = GetComponent<PlayerStatus>();
    }


    private void Update()
    {
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
        
        if (IsNormalAttack==true)
        {
            if (Target_normalattack == null) return;
            float distance = Vector3.Distance(transform.position, Target_normalattack.position);
            
            if (distance <= PS.AttackDistance)
            {
                PS.AttackTimer += Time.deltaTime;
                if (PS.AttackTimer >= (1f / PS.AttackRate))
                {
                    Target_normalattack.gameObject.GetComponent<EnemyAct>().TakeDamage(PS.AD);
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

}
