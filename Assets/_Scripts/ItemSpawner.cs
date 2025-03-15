using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner Instance;

    public Vector3 spawnPosition;

    public GameObject letterPrefab;
    public GameObject goldPrefab;
    public GameObject healthPrefab;

    public Sprite[] letterSprites;
    
    
    
    
    
    void Start()
    {
        StartCoroutine(SpawnItemRoutine());
    }

    

    IEnumerator SpawnItemRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 3));
            
            float spawnRate = Random.Range(0, 1);
            if (spawnRate < 0.2f) Instantiate(goldPrefab, spawnPosition, default);
            if (spawnRate < 0.5f) SpawnLetter();
            else Instantiate(healthPrefab, spawnPosition, default);
        }
    }

    private void SpawnLetter()
    {
        float spawnTrueLetterRate = Random.Range(0, 1);
        if (spawnTrueLetterRate < 0.2f)
        {
            char trueLetter = GameManager.Instance.currentAnswer[GameManager.Instance.currentLetterIndex];
            Letter letterScript = Instantiate(letterPrefab, spawnPosition, default).GetComponent<Letter>();
            //if(letterScript != null) letterSprites.SetL
        }
    }

    public void SpawnHintLetter()
    {
        
    }
}
