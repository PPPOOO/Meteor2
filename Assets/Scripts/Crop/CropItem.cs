using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CropItem :MonoBehaviour
{
    public int ID;
    public SpriteRenderer SR;
    public ItemSeed Seed;
    public float CurrentGrow=0;

    public float MaxGrow;
    public float DayGrow;

    public bool IsGrown = false;
    public List<string> DiffSprites;

    public void SetID(int id)
    {
        SR = GetComponent<SpriteRenderer>();
        ID = id;

        CropManager.Instance.AddCrop(this);
        Seed = InventoryManager.Instance.GetItemById(ID) as ItemSeed;
        SR.sprite = Resources.Load<Sprite>(Seed.Sprite);
        DiffSprites= Seed.Sprites;
        MaxGrow = Seed.Maxgrow;
        DayGrow = Seed.Daygrow;
    }

    public void Water(float value)
    {
        CurrentGrow += value * DayGrow;
    }

    public void Fertilize(float value)
    {
        CurrentGrow += value * DayGrow;
    }

    public void Harvest(float value)
    {
        if (IsGrown == true)
        {
            CropHarvestChestPanel.Instance.StoreItem(Seed.ProductID);
            Debug.Log(Seed.ProductID);
            //收获箱中获得产物
            CropManager.Instance.RemoveGrowCrop(this);
            Destroy(gameObject);
        }
        
        
    }

    public void Grow()
    {
        if (CurrentGrow / MaxGrow < 0.4f && CurrentGrow / MaxGrow > 0.2f)
        {
            SR.sprite = Resources.Load<Sprite>(DiffSprites[0]);
        }
        else if (CurrentGrow / MaxGrow < 0.6f && CurrentGrow / MaxGrow > 0.4f)
        {
            SR.sprite = Resources.Load<Sprite>(DiffSprites[1]);
        }
        else if (CurrentGrow / MaxGrow < 0.8f && CurrentGrow / MaxGrow > 0.6f)
        {
            SR.sprite = Resources.Load<Sprite>(DiffSprites[2]);
        }
        else if (CurrentGrow >= MaxGrow)
        {
            SR.sprite = Resources.Load<Sprite>(DiffSprites[3]);
            IsGrown = true;
            CropManager.Instance.AddGrowCrop(this);
            CropManager.Instance.RemoveCrop(this);
        }
        
    }


    
}
