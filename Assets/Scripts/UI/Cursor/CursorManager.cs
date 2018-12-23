using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CursorManager : MonoBehaviour
{

    private static CursorManager _instance;
    public static CursorManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag("Manager").GetComponent<CursorManager>();
            }
            return _instance;
        }
    }

    public Texture2D mCursor_normal;
    public Texture2D mCursor_NPC_Talk;
    public Texture2D mCursor_Attack;
    public Texture2D mCursor_LockTarget;
    public Texture2D mCursor_Pick;

    private Vector2 mHostPos = Vector2.zero;
    private CursorMode mode = CursorMode.Auto;

    public void SetNormal()
    {
        Cursor.SetCursor(mCursor_normal, mHostPos, mode);
    }
    
    public void SetNPCTalk()
    {
        Cursor.SetCursor(mCursor_NPC_Talk, mHostPos, mode);
    }
    public void SetAttack()
    {
        Cursor.SetCursor(mCursor_Attack, mHostPos, mode);
    }
    public void SetLockTarget()
    {
        Cursor.SetCursor(mCursor_LockTarget, mHostPos, mode);
    }
}
