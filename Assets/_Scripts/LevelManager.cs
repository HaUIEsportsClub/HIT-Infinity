using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public bool IsLevelUnlocked(int level)
    {
        int defaultValue;
        if (level == 1)
        {
            defaultValue = 1;
        }
        else
        {
            defaultValue = 0;
        }
        int unlocked = PlayerPrefs.GetInt("Level_" + level + "_Unlocked", defaultValue);
        return (unlocked == 1);
    }

    public void UnlockLevel(int level)
    {
        PlayerPrefs.SetInt("Level_" + level + "_Unlocked", 1);
        PlayerPrefs.Save();
    }

    public void LoadLevel(int level)
    {
        if (IsLevelUnlocked(level))
        {
            PlayerPrefs.SetInt("CurrentLevelIndex", level);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Level" + level);
        }
    }
 

}