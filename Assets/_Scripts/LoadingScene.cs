using System;

using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private Slider slideBar;
    [SerializeField] private Button playBtn;
    
    [Header("__________HITINF__________")]
    [SerializeField] private Transform hitInf;

    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private Vector3 targetPosition;

    private void LoadComponent()
    {
        if (slideBar == null) slideBar = transform.GetComponentInChildren<Slider>();
        if (hitInf == null) hitInf = transform.Find("HITINF");
        if (playBtn == null) playBtn = transform.GetComponentInChildren<Button>();
        slideBar.value = 0;
        
        hitInf.position = originalPosition;
        hitInf.localScale *= 0.5f;
        hitInf.gameObject.SetActive(false);

        playBtn.transform.localScale *= 0;
        
    }

    private void Reset()
    {
        LoadComponent();
    }

    private void Awake()
    {
        LoadComponent();
    }

    private void Start()
    {
        playBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("LevelSelection");
        });
    }

    private void Update()
    {
        float additionSiderBar;
        if (slideBar.value < 0.7f) additionSiderBar = Time.deltaTime / 10;
        else if (slideBar.value >= 0.7f && slideBar.value < 0.9f) additionSiderBar = Time.deltaTime / 20;
        else additionSiderBar = Time.deltaTime;
        slideBar.value = Mathf.Min(1, slideBar.value + additionSiderBar);
        if (Math.Abs(slideBar.value - 1) < 0.001f)
        {
            slideBar.gameObject.SetActive(false);
            
            hitInf.gameObject.SetActive(true);

          
            hitInf.DOScale(1, 0.75f);
            hitInf.DOMove(targetPosition, 0.75f).OnComplete(() =>
            {
                playBtn.transform.DOScale(1, 0.75f);
            });
           
        } 
    }
}
