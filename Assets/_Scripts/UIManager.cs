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
    public GameObject settingPanel;

    [Header("UI Elements")]
    public Button BuyHintButton;
    public Button OpenHintPanelButton;
    public TextMeshProUGUI hintPriceText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI timerText;

    public Button nextLevelButton;
    public Button pauseButton;
    public Button settingButton;
    [Header("Health UI")]
    public Image[] heartIcons;

    public Sprite whiteHeart;
    public Sprite redHeart;
    
    public Image starPlaceholder1;
    public Image starPlaceholder2;
    public Image starPlaceholder3;
    public Sprite starFilled;
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


        for (int i = 0; i < heartIcons.Length; i++)
        {
            if (i < health)
            {
                heartIcons[i].sprite = redHeart;

            }
            else
            {
                heartIcons[i].sprite = whiteHeart;
            }
        }
    }

    public void UpdateGoldUI(int gold)
    {
        if (goldText != null)
        {
            goldText.text = gold.ToString();
        }
    }
    public void UpdateTimeUI(float time)
    {
        if (timerText != null)
            timerText.text = Mathf.CeilToInt(time).ToString() + "s";
    }
    public IEnumerator ShowWinPanelDelayed(float delay, bool showNextButton)
    {
        yield return new WaitForSeconds(delay);
        nextLevelButton.gameObject.SetActive(showNextButton);
        winPanel.SetActive(true);
    }
    //Hint Panel
    public void ShowHintPanel()
    {
        hintPanel.SetActive(true);
        Time.timeScale = 0;

        int currentGold = GoldManager.Instance.gold;
        if (currentGold >= 10)
        {
            BuyHintButton.interactable = true;
            BuyHintButton.GetComponent<Image>().color = Color.green; // Màu xanh
            BuyHintButton.GetComponentInChildren<TextMeshProUGUI>().text = "10 Gold";
        }
        else
        {
            BuyHintButton.interactable = false;
            BuyHintButton.GetComponent<Image>().color = Color.gray; // Màu xám
            BuyHintButton.GetComponentInChildren<TextMeshProUGUI>().text = "10 Gold";

        }
    }
    public void HideHintPanel()
    {
        hintPanel.SetActive(false);
        Time.timeScale = 1;
    }
    //PausePanel
    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void TogglePause()
    {
        if (pausePanel.activeSelf)
            HidePausePanel();
        else
            ShowPausePanel();
    }
    //Setting Panel
    public void ShowSettingPanel()
    {
        settingPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void HideSettingPanel()
    {
        settingPanel.SetActive(false) ;
        Time.timeScale = 1;
    }
    public void DisplayStars(int starRating)
    {
        if (starRating >= 1)
            starPlaceholder1.sprite = starFilled;
        else
        {
            starPlaceholder2.gameObject.SetActive(false);
            starPlaceholder3.gameObject.SetActive(false);
            
        }

        if (starRating >= 2)
        {
            starPlaceholder1.sprite = starFilled;
            starPlaceholder2.sprite = starFilled;
            
        }
        else
        {
            starPlaceholder3.gameObject.SetActive(false);
            
        }

        if (starRating >= 3)
        {
            starPlaceholder1.sprite = starFilled;
            starPlaceholder2.sprite = starFilled;
            starPlaceholder3.sprite = starFilled;
            
        }
       

    }
}
