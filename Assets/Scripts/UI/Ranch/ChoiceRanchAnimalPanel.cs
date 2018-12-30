using UnityEngine;
using System.Collections;

public class ChoiceRanchAnimalPanel :BaseInventoryPanel
{
    private static ChoiceRanchAnimalPanel _instance;
    public static ChoiceRanchAnimalPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Canvas/RanchPanel/ChoiceRanchAnimalPanel").GetComponent<ChoiceRanchAnimalPanel>();
            }
            return _instance;
        }
    }


    public override void Show()
    {
        if (transform.Find("Scroll View/Viewport/Content").childCount != 0)
        {
            RanchAnimalItemChoice[] ranchAnimalItemChoices;
            ranchAnimalItemChoices = transform.Find("Scroll View/Viewport/Content").GetComponentsInChildren<RanchAnimalItemChoice>();
            foreach (RanchAnimalItemChoice ranchAnimalItemChoice in ranchAnimalItemChoices)
            {
                ranchAnimalItemChoice.DestroySelf();
            }
        }
        RanchnManager.Instance.CreatChoiceRanchAnimal();
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }
}
