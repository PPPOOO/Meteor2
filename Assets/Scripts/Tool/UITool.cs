using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITool
{

    public static GameObject FindChild(GameObject parent, string childName)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>();
        bool isFinded = false;
        Transform child = null;
        foreach (Transform t in children)
        {
            if (t.name == childName)
            {
                if (isFinded)
                {
                    Debug.LogWarning("在游戏物体中" + parent + "下存在不止一个子物体" + childName);
                }
                isFinded = true;
                child = t;
            }
        }
        if (isFinded) return child.gameObject;
        return null;
    }

    public static T FindChild<T>(GameObject parent, string childName)
    {
        GameObject uiGO = FindChild(parent, childName);
        if (uiGO == null)
        {
            Debug.LogError("在游戏物体" + parent + "查找不到" + childName);
            return default(T);
        }
        return uiGO.GetComponent<T>();
    }

    public static Button GetButton(GameObject parent, string childName)
    {
        GameObject gameObject = FindChild(parent, childName);
        Button btn = gameObject.GetComponent<Button>();
        return btn;
    }
}
