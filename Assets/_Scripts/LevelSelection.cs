using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] levelButtons; 
    public GameObject starPrefab;
    private void Start()
    {
        
        if (PlayerPrefs.GetInt("FirstRun", 1) == 1) 
        {
            ResetGameData();
            PlayerPrefs.SetInt("FirstRun", 0); 
            PlayerPrefs.Save();
        }
        UpdateLevelButtons();
        //ResetGameData();
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
            
            SetupLevelStars(levelIndex, levelButtons[i].transform);
        }
    }
    private void SetupLevelStars(int levelIndex, Transform buttonTransform)
    {
        Transform starPanel = buttonTransform.Find("StarPanel");
        if (starPanel == null)
        {
            Debug.LogWarning("không tìm thấy StarPanel trên button level " + levelIndex);
            return;
        }
        foreach (Transform child in starPanel)
        {
            Destroy(child.gameObject);
        }
        int starRating = PlayerPrefs.GetInt("Level" + levelIndex + "Stars", 0);
       
        Debug.Log("Level " + levelIndex + " đạt " + starRating + " sao.");
        
        for (int i = 0; i < starRating; i++)
        {
            Debug.Log("spawn star");
            GameObject starObj = Instantiate(starPrefab, starPanel );
            starObj.name = "Star_" + i;
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