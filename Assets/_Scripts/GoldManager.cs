using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance;
    public int gold = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadGold();
       
    }

    public void AddGold(int amount)
    {
        gold += amount;
        SaveGold();
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateGoldUI(gold);
        }
    }

    public void SaveGold()
    {
        PlayerPrefs.SetInt("Gold", gold);
        PlayerPrefs.Save();
    }

    public void LoadGold()
    {
        gold = PlayerPrefs.GetInt("Gold", 0);
    }
}