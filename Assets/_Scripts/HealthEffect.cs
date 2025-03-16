using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEffect : MonoBehaviour
{
   
    private void OnEnable()
    {
        StartCoroutine(WaitForSetActive());
    }

    IEnumerator WaitForSetActive()
    {
        yield return new WaitForSeconds(0.4f);
        gameObject.SetActive(false);
    }
}
