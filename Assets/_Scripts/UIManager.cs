using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("UI Panels")]
    public GameObject winPanel;     
    public GameObject gameOverPanel; 
    public GameObject pausePanel;   
    public GameObject hintPanel;    

    [Header("UI Elements")]
    public Button buyHintButton;          
    public Button openHintPanelButton; 
    public TextMeshProUGUI hintPriceText;
    public TextMeshProUGUI winText;      
    public TextMeshProUGUI goldText;     
    public Button nextLevelButton;    
    public Button pauseButton; 
    [Header("Health UI")]
    public Image[] heartIcons;

    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthUI(int health)
    {
        
    }

    public void UpdateGoldUI(int gold)
    {
        
    }

    public IEnumerator ShowWinPanelDelayed(float delay, string message, bool showNextButton)
    {
        return null;
    }

    public void ShowHintPanel()
    {
        
    }

    public void HideHintPanel()
    {
        
    }

    public void ShowPausePanel()
    {
        
    }
    public void HidePausePanel()
    {
        
    }

    public void TogglePause()
    {
        
    }
    


    
}
