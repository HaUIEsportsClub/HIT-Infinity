using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string currentAnswer= "VIETNAM";
    public int currentLetterIndex=0;
    public Transform letterPanel;
    public GameObject letterSlotPrefab;
    public bool gameActive = true; 
    private Transform[] letterSlots;
    public float timeLeft = 100f;

    [Header("__________From shop__________")]
    public SpriteRenderer BackGroundSR;
    public ShopData shopData;
    public int SkinId = 0;

    public int BackgroundId = 0;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
    
      
        SetupLetterPanel();
        UIManager.Instance.UpdateTimeUI(timeLeft);

        if (shopData != null)
        {
            PlayerController.Instance.PlayerSR.sprite = shopData.SkinParams[SkinId].ProductSprite;
            if (BackGroundSR != null) BackGroundSR.sprite = shopData.BackgroundParams[BackgroundId].ProductSprite;
        }
        
    }
    void Update()
    {
        if (!gameActive) return;
        
        // Giảm thời gian còn lại
        timeLeft -= Time.deltaTime;
        UIManager.Instance.UpdateTimeUI(timeLeft);

        if (timeLeft <= 0)
        {
            GameOver();
        }
    }
  
    public void AddTime(float seconds)
    {
        timeLeft += seconds;
        UIManager.Instance.UpdateTimeUI(timeLeft);
    }
    public void GameOver()
    {
        gameActive = false;
        AudioManager.PlaySound(AudioManager.SoundId.GameOver);
        UIManager.Instance.gameOverPanel.SetActive(true);
    }
    
    void SetupLetterPanel()
    {
        int length = currentAnswer.Length;
        letterSlots = new Transform[length];

        for (int i = 0; i < length; i++)
        {
            GameObject slotObj = Instantiate(letterSlotPrefab, letterPanel);
            letterSlots[i] = slotObj.transform;
        }
    }

    public void CheckLetter(GameObject letterObj)
    {
        Letter letterScript = letterObj.GetComponent<Letter>();


        char pickedChar = letterScript.m_LetterChar;
        char neededChar = currentAnswer[currentLetterIndex];

        if (pickedChar == neededChar)
        {
            AudioManager.PlaySound(AudioManager.SoundId.CorrectAnswer);
            Transform targetSlot = letterSlots[currentLetterIndex];

            letterObj.transform
                .DOMove(targetSlot.position, 0.5f)
                .OnComplete(() =>
                {
                    letterObj.transform.SetParent(targetSlot);
                    letterScript.StopMoving();
                });

            currentLetterIndex++;

            if (currentLetterIndex >= currentAnswer.Length)
            {
                HandleWin();
              
            }
        }
        else
        {
            if (PlayerController.Instance != null)
            {
                AudioManager.PlaySound(AudioManager.SoundId.InCorrect);
                PlayerController.Instance.TakeDamage();
            }

            Destroy(letterObj);
        }
    }
    private void HandleWin()
    {
        AudioManager.PlaySound(AudioManager.SoundId.Win);
        gameActive = false;
        UIManager.Instance.OpenHintPanelButton.interactable = false;
        UIManager.Instance.pauseButton.interactable = false;
        

        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentLevelIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
         
            LevelManager.Instance.UnlockLevel(currentLevelIndex + 1);
            StartCoroutine(UIManager.Instance.ShowWinPanelDelayed(1f , true));
        }
        else
        {
            StartCoroutine(UIManager.Instance.ShowWinPanelDelayed(1f, false));
        }

       
    }

    public void BuyHint()
    {
        int currentGold = GoldManager.Instance.gold;
        if (currentGold >= 10)
        {
            GoldManager.Instance.AddGold(-10);


            char correctLetter = currentAnswer[currentLetterIndex];

            GameObject hintLetterObj = ItemSpawner.Instance.SpawnHintLetter(correctLetter);

          
            CheckLetter(hintLetterObj);
            UIManager.Instance.HideHintPanel();
        }
    }
    public void RetryLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToLevelSelection()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelSelection");
    }


    public void LoadNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;


        SceneManager.LoadScene(currentLevelIndex + 1);
    }

  
   
}