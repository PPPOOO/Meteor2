using UnityEngine;
using System.Collections;

public class RanchAnimalUI : MonoBehaviour
{
    public int ID;
    public RanchAnimalInfo RanchAnimalInfo;
    public int DayGrow;
    public int MaxGrow;
    public bool IsGrow;
    public int CurrentGrow = 0;

    protected Animator animator;
    public float MoveSpeed = 1;
    public int i = -1;
    public Vector3 RandomDir;
    public Vector3 RandomTargetPos;



    // Use this for initialization
    void Start()
    {

    }


    public virtual void SetID(int id)
    {
        Debug.Log(id);
        animator = GetComponent<Animator>();
        ID = id;
        RanchAnimalInfo = RanchnManager.Instance.GetRanchAnimalInfoByID(ID);
        RanchnManager.Instance.RanchAnimalUIList.Add(this);

        
        GetAnimation(RanchAnimalInfo.Animation);
        RandomMove();
    }
    // Update is called once per frame

    public void Pacify()
    {
        DayGrow += 20;
    }

    public void Feed()
    {
        DayGrow += 30;
    }

    public void Harvest()
    {
        if (IsGrow == false) return;
        else
        {
            RanchHarvestChestPanel.Instance.StoreItem(InventoryManager.Instance.GetItemById(RanchAnimalInfo.ProductID));
            CurrentGrow = 0;
        }
    }

    public void Grow()
    {
        if (CurrentGrow >= MaxGrow)
        {
            //IsGrow = true;
            //RanchnManager.Instance.AddGrowCrop(this);
            //RanchnManager.Instance.RemoveCrop(this);
        }
    }



    #region 随机上下左右移动和得到动画
    public void RandomMove()
    {
        InvokeRepeating("GetRandomTargetPos", 0, 5);
        InvokeRepeating("MoveToRandomTargetPos", 0, 0.02f);
    }


    public void GetRandomTargetPos()
    {
        i = Random.Range(0, 4);
        if (i == 0)
        {
            RandomDir = new Vector3(0, 1);
        }
        if (i == 1)
        {
            RandomDir = new Vector3(0, -1);
        }
        if (i == 2)
        {
            RandomDir = new Vector3(1, 0);
        }
        if (i == 3)
        {
            RandomDir = new Vector3(-1, 0);
        }
        animator.SetFloat("x", RandomDir.x);
        animator.SetFloat("y", RandomDir.y);
        RandomTargetPos = transform.position + RandomDir;
    }

    public void MoveToRandomTargetPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, RandomTargetPos, MoveSpeed * Time.deltaTime);
    }

    public void GetAnimation(string RanchAnimal_Resources_Name)
    {
        string AnimationPath = "Animation/Animation/RanchAnimal/";
        animator = GetComponent<Animator>();
        AnimatorOverrideController overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;
        overrideController["Up"] = Resources.Load<AnimationClip>(AnimationPath + RanchAnimal_Resources_Name + "/Up");
        overrideController["Right"] = Resources.Load<AnimationClip>(AnimationPath + RanchAnimal_Resources_Name + "/Right");
        overrideController["Left"] = Resources.Load<AnimationClip>(AnimationPath + RanchAnimal_Resources_Name + "/Left"); ;
        overrideController["Down"] = Resources.Load<AnimationClip>(AnimationPath + RanchAnimal_Resources_Name + "/Down");

    }
    #endregion
}
