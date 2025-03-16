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
    [Header("__________Moon__________")]
    [SerializeField] private Transform moon;

    [SerializeField] private float moonTime = 0;
   
    private void LoadComponent()
    {
        if (slideBar == null) slideBar = transform.GetComponentInChildren<Slider>();
        if (hitInf == null) hitInf = transform.Find("HITINF");
        if (playBtn == null) playBtn = transform.GetComponentInChildren<Button>();
        if (moon == null) moon = transform.Find("Moon");
        moon.position = new Vector3(-7, 2, 0);
        moonTime = 0;
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
        AudioManager.PlayBackgroundSound(AudioManager.SoundId.Loading);
        playBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("LevelSelection");
        });
    }

    private void Update()
    {
        
        UpdateSliderBar();
        moonTime = Mathf.Min(1, moonTime += Time.deltaTime / 12);
        MoveMoon(moonTime);
        if (Math.Abs(slideBar.value - 1) < 0.001f)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                
                SceneManager.LoadScene("LevelSelection");
               
            }
                   
            slideBar.gameObject.SetActive(false);
            
            hitInf.gameObject.SetActive(true);

          
            hitInf.DOScale(1, 0.75f);
            hitInf.DOMove(targetPosition, 0.75f).OnComplete(() =>
            {
                playBtn.transform.DOScale(0.3f, 0.9f);
            });
           
        } 
    }

    private void UpdateSliderBar()
    {
        float additionSiderBar;
        if (slideBar.value < 0.7f) additionSiderBar = Time.deltaTime / 5;
        else if (slideBar.value >= 0.7f && slideBar.value < 0.9f) additionSiderBar = Time.deltaTime / 20;
        else additionSiderBar = Time.deltaTime;
        slideBar.value = Mathf.Min(1, slideBar.value + additionSiderBar);
    }

    private void MoveMoon(float x)
    {
        moon.position = new Vector3(FX(x), FY(x), 0);
    }

    private float FX(float x)
    {
        return 14 * x - 7;
    }

    private float FY(float x)
    {
        return -6 * x * x + 6 * x + 2;
    }

    
}
