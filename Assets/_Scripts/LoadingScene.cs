using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private Slider slideBar;

    private void LoadaComponent()
    {
        if (slideBar == null) slideBar = transform.GetComponentInChildren<Slider>();
    }
    
}
