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
        KeyToMove();
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


}
