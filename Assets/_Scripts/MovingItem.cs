using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingItem : MonoBehaviour
{
    public float moveSpeed = 2f;
    private bool isMoving = true; 
    public void StopMovement()
    {
        isMoving = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
