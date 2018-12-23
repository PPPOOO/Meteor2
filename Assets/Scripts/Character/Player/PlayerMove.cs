using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{


    private PlayerStatus ps;
    private float move_distance = 1f;
    private Animator animator;
    private Vector3 targetpos;
    private Vector3 xiangdui_pos;

    

   void Start()
    {

       ps = GetComponent<PlayerStatus>();

       animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        if (Input.GetMouseButton(1) && MonoBehaviourTool.Instance.GetOverUI() == false)
        {
            MouseFollow();
        }
        else
        {
            KeyToMove();
        }
        //else if (Input.GetKey(KeyCode.W))
        //{
        //    if (Input.GetKey(KeyCode.A))
        //    {
        //        targetpos = new Vector3(transform.position.x - move_distance, transform.position.y + move_distance, transform.position.z);
        //    }
        //    else if (Input.GetKey(KeyCode.D))
        //    {
        //        targetpos = new Vector3(transform.position.x + move_distance, transform.position.y + move_distance, transform.position.z);
        //    }
        //    else
        //    {
        //        targetpos = new Vector3(transform.position.x, transform.position.y + move_distance, transform.position.z);
        //    }
        //}
        //else if (Input.GetKey(KeyCode.S))
        //{
        //    if (Input.GetKey(KeyCode.A))
        //    {
        //        targetpos = new Vector3(transform.position.x - move_distance, transform.position.y - move_distance, transform.position.z);
        //    }
        //    else if (Input.GetKey(KeyCode.D))
        //    {
        //        targetpos = new Vector3(transform.position.x + move_distance, transform.position.y - move_distance, transform.position.z);
        //    }
        //    else
        //    {
        //        targetpos = new Vector3(transform.position.x, transform.position.y - move_distance, transform.position.z);
        //    }
        //}
        //else if (Input.GetKey(KeyCode.A))
        //{
        //    targetpos = new Vector3(transform.position.x - move_distance, transform.position.y, transform.position.z);
        //}
        //else if (Input.GetKey(KeyCode.D))
        //{
        //    targetpos = new Vector3(transform.position.x + move_distance, transform.position.y, transform.position.z);
        //}
        //transform.position = Vector3.MoveTowards(transform.position, targetpos, ps.MoveSpeed * 0.03f);
    }

    public void KeyToMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (y != 0)
        {
            animator.SetFloat("y", y);
            animator.SetFloat("x", 0);
        }
        else
        {
            animator.SetFloat("y", 0);
            animator.SetFloat("x", x);
        }
        Vector3 targetpos = new Vector3(x, y);
        transform.position = transform.position + targetpos*Time.deltaTime*ps.MoveSpeed;
    }

    void MouseFollow()
    {

        targetpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        targetpos.z = 0;
        xiangdui_pos = targetpos - transform.position;
        if (xiangdui_pos.y > 0 && xiangdui_pos.x > 0)
        {
            if (xiangdui_pos.y > xiangdui_pos.x)
            {
                animator.SetTrigger("walkup");
            }
            else
            {
                animator.SetTrigger("walkright");
            }
        }
        if (xiangdui_pos.y > 0 && xiangdui_pos.x < 0)
        {
            if (xiangdui_pos.y > -xiangdui_pos.x)
            {
                animator.SetTrigger("walkup");
            }
            else
            {
                animator.SetTrigger("walkleft");
            }
        }
        if (xiangdui_pos.y < 0 && xiangdui_pos.x > 0)
        {
            if (-xiangdui_pos.y > xiangdui_pos.x)
            {
                animator.SetTrigger("walkdown");
            }
            else
            {
                animator.SetTrigger("walkright");
            }
        }
        if (xiangdui_pos.y < 0 && xiangdui_pos.x < 0)
        {
            if (-xiangdui_pos.y > -xiangdui_pos.x)
            {
                animator.SetTrigger("walkdown");
            }
            else
            {
                animator.SetTrigger("walkleft");
            }
        }

    }

}
