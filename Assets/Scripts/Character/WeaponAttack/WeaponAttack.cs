using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    private PlayerStatus PS;
    public float RotateTime = 0.5f;
    public float DestroyTime = 0;
    public WeaponDir weaponDir;
    private bool IsSetStartPos = false;
    private PolygonCollider2D PolygonCollider2D;
    private List<CharacetStatus> characetStatuses = new List<CharacetStatus>();

    public enum WeaponDir
    {
        Up,
        Down,
        Right,
        Left
    }

    public float RotateSpeed;

    private void Start()
    {
        PolygonCollider2D = GetComponent<PolygonCollider2D>();
        PS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        if (IsSetStartPos == false)
        {
            SetStartPos();
        }
        RotateSpeed = (100 / RotateTime);
        transform.Rotate(Vector3.forward, RotateSpeed * Time.deltaTime);



        DestroyTime += Time.deltaTime;
        if (DestroyTime >= RotateTime)
        {
            Destroy(gameObject);
        }
    }
    private void SetStartPos()
    {
        switch (weaponDir)
        {
            case WeaponDir.Up:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case WeaponDir.Down:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case WeaponDir.Right:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case WeaponDir.Left:

                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }
        IsSetStartPos = true;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            CharacetStatus characetStatus = col.GetComponent<CharacetStatus>();
            int index = characetStatuses.IndexOf(characetStatus);
            if (index == -1)
            {
                col.GetComponent<CharacetStatus>().HPRemainChange(-PS.AD);
                characetStatuses.Add(characetStatus);
            }
        }
    }
}
