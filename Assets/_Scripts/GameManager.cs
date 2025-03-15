using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string currentAnswer= "VIETNAM";
    public int currentLetterIndex=0;
    public Transform letterPanel;
    public GameObject letterSlotPrefab;

    public bool gameActive = true; 
    public Transform[] letterSLots;

    private void Awake()
    {
        Instance = this;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        SetUpLetterPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetUpLetterPanel()
    {
        int length = currentAnswer.Length;
        letterSLots = new Transform[length];
        for (int i = 0; i < length; i++)
        {
            GameObject slotObj = Instantiate(letterSlotPrefab, letterPanel);
            letterSLots[i] = slotObj.transform;
        }
    }

    public void CheckLetter(GameManager letterObj)
    {
       
    }

    public void HandleWin()
    {
        
    }
    private void BuyHint()
    {
        
    }

    public void RetryLevel()
    {
        
    }

    public void GotoLevelSelection()
    {
        
    }

    public void LoadLevelIndex()
    {
        
    }
    

   
    
}
