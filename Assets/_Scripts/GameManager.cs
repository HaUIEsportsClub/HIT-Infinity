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
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
    
      
        SetupLetterPanel();
        
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
                PlayerController.Instance.TakeDamage();
            }

            Destroy(letterObj);
        }
    }
    private void HandleWin()
    {
      
        gameActive = false;
        UIManager.Instance.OpenHintPanelButton.interactable = false;
        UIManager.Instance.pauseButton.interactable = false;
        

        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentLevelIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
         
            LevelManager.Instance.UnlockLevel(currentLevelIndex + 1);
            StartCoroutine(UIManager.Instance.ShowWinPanelDelayed(1f, "Bạn đã chiến thắng!", true));
        }
        else
        {
            StartCoroutine(UIManager.Instance.ShowWinPanelDelayed(1f, "Bạn đã chiến thắng trò chơi!", false));
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