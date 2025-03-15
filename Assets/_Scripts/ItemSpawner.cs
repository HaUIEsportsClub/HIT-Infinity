using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner Instance;

    public GameObject letterPrefab;
    public GameObject goldPrefab;
    public GameObject healthPrefab;

    public Sprite[] letterSprites;
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnItemRoutine()
    {
        return null;
    }

    public void SpawnHintLetter()
    {
        
    }
}
