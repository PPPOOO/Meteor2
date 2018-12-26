using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : CharacetStatus
{
    public int ID;
    public Enemy enemy;
    public Animator animator;
    private GameObject HUDPrefab;
    private Transform HUDPanel;


    public virtual void SetID(int id)
    {
        HUDPrefab = Resources.Load<GameObject>("HUDText");
        HUDPanel = GameObject.FindGameObjectWithTag("Canvas").transform.Find("HUDPanel");
        animator = GetComponent<Animator>();

        ID = id;
        enemy = EnemyManager.Instance.GetEnemyById(ID);
        for(int i = 0; i < enemy.ApplyAttrEffects.Count; i++)
        {
            AttrManager.Instance.ChangeAttrByType(gameObject, enemy.ApplyAttrEffects[i]);
        }
        EnemyManager.Instance.enemyUIsList.Add(this);
        HP_Remain = HP;
        GetAnimation(enemy.Animation);
    }



    public void GetAnimation(string NPC_Resources_Name)
    {
        string AnimationPath = "Animation/Animation/Enemy/";
        AnimatorOverrideController overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;
        overrideController["Up"] = Resources.Load<AnimationClip>(AnimationPath + NPC_Resources_Name + "/Up");
        overrideController["Right"] = Resources.Load<AnimationClip>(AnimationPath + NPC_Resources_Name + "/Right");
        overrideController["Left"] = Resources.Load<AnimationClip>(AnimationPath + NPC_Resources_Name + "/Left"); ;
        overrideController["Down"] = Resources.Load<AnimationClip>(AnimationPath + NPC_Resources_Name + "/Down");
        overrideController["Dead"] = Resources.Load<AnimationClip>(AnimationPath + NPC_Resources_Name + "/Dead");

    }
}
