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
    public Button BuyHintButton;          
    public Button OpenHintPanelButton; 
    public TextMeshProUGUI hintPriceText;
    public TextMeshProUGUI winText;     
    public TextMeshProUGUI goldText;    
    public Button nextLevelButton;     
    public Button pauseButton; 
    [Header("Health UI")]
    public Image[] heartIcons;         
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        int currentGold = PlayerPrefs.GetInt("Gold", 0);
        UpdateGoldUI(currentGold);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    public void UpdateHealthUI(int health)
    {
       

      
    }

    public void UpdateGoldUI(int gold)
    {
     
    }
    public IEnumerator ShowWinPanelDelayed(float delay, string message, bool showNextButton)
    { 
      
    }
    public void ShowHintPanel()
    {
       
    }
    public void HideHintPanel()
    {
       
    }
    public void ShowPausePanel()
    {


    public void HidePausePanel()
    {
      
    }
    public void TogglePause()
    {
       
    }
}
