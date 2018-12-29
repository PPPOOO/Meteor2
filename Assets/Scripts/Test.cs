using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(enumerator());
        }
    }

    IEnumerator enumerator()
    {
        for(int i = 0; i <= 99; i++)
        {
            Debug.Log(i);
            yield return new WaitForSeconds(1);
        }
        

    }
}
