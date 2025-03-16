using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner Instance;
    [Header("Cài đặt Spawn")]
    public GameObject letterPrefab;     
    public GameObject healthPrefab;    
    public GameObject goldPrefab;   
    public GameObject clockPrefab;
    public Transform spawnPoint;       

    [Header("Sprites chữ A-Z")]
    public Sprite[] letterSprites;     

    public float spawnIntervalMin = 2f;
    public float spawnIntervalMax = 3f;
    private void Awake()
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
        StartCoroutine(SpawnItemRoutine());
    }

    IEnumerator SpawnItemRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));

            if (GameManager.Instance.gameActive == false)   yield break;

            
            float spawnRoll = Random.value; 

            if (spawnRoll < 0.15f) Instantiate(healthPrefab, spawnPoint.position, Quaternion.identity);
            else if (spawnRoll < 0.3f) Instantiate(goldPrefab, spawnPoint.position, Quaternion.identity);
            else if (spawnRoll < 0.5f) Instantiate(clockPrefab, spawnPoint.position, Quaternion.identity);
            
            else SpawnLetterObject();
        }
    }

    private void SpawnLetterObject()
    {
        bool spawnCorrect = (Random.value < 0.2f);
        char letterToSpawn;
        Sprite spriteToSpawn;

                
        char correctLetter = GameManager.Instance.currentAnswer[GameManager.Instance.currentLetterIndex];

        if (spawnCorrect)
        {
                    
            letterToSpawn = correctLetter;
            int index = letterToSpawn - 'A';
            spriteToSpawn = letterSprites[index];
        }
        else
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, letterSprites.Length);
            } while ((char)('A' + randomIndex) == correctLetter);

            letterToSpawn = (char)('A' + randomIndex);
            spriteToSpawn = letterSprites[randomIndex];
        }

                
        GameObject letterObj = Instantiate(letterPrefab, spawnPoint.position, Quaternion.identity);
        Letter letterScript = letterObj.GetComponent<Letter>();

        if (letterScript != null) letterScript.SetLetter(spriteToSpawn, letterToSpawn);
    }
    public GameObject  SpawnHintLetter(char correctLetter)
    {
        int index = correctLetter - 'A'; 
        if (index < 0 || index >= letterSprites.Length) return null;

        Sprite correctSprite = letterSprites[index];

        
        GameObject letterObj = Instantiate(letterPrefab, spawnPoint.position, Quaternion.identity);
        Letter letterScript = letterObj.GetComponent<Letter>();

        if (letterScript != null)
        {
           
            letterScript.SetLetter(correctSprite, correctLetter);
        }
        return letterObj;
    }
   

}
