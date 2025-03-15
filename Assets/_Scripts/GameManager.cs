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
            
            Debug.Log("spawn slot");
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
    }

    public void BuyHint()
    {
        
    }
    public void RetryLevel()
    {
       
    }

    public void GoToLevelSelection()
    {
       
    }


    public void LoadNextLevel()
    {
       
    }

  
   
}