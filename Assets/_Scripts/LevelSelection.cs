using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] levelButtons; 

    private void Start()
    {
        
        
        if (PlayerPrefs.GetInt("FirstRun", 1) == 1) 
        {
            ResetGameData();
            PlayerPrefs.SetInt("FirstRun", 0); 
            PlayerPrefs.Save();
        }
        UpdateLevelButtons();
       
    }

    
    private void UpdateLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1; 
            
            
            levelButtons[i].onClick.RemoveAllListeners();

           
            if (LevelManager.Instance.IsLevelUnlocked(levelIndex))
            {
                SetButtonUnlocked(i,levelIndex);
            }
            else SetButtonLocked(i);
            
        }
    }

    private void SetButtonUnlocked(int buttonIndex, int levelIndex)
    {
        levelButtons[buttonIndex].interactable = true; 
        int capturedIndex = levelIndex;    
        levelButtons[buttonIndex].onClick.AddListener(() => OnLevelButtonClick(capturedIndex));
    }

    private void SetButtonLocked(int buttonIndex)
    {
        levelButtons[buttonIndex].interactable = false;
        levelButtons[buttonIndex].GetComponent<Image>().color = Color.gray; 
    }

   
    private void OnLevelButtonClick(int level)
    {
        LevelManager.Instance.LoadLevel(level);
    }

    public void LoadShop()
    {
        SceneManager.LoadScene("Shopping");

    }
    public void ResetGameData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Game data has been reset.");
    }
    
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}