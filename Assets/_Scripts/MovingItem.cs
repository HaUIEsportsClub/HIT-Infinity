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
    
     
        
         void Update()
         {
             if (GameManager.Instance != null && !GameManager.Instance.gameActive)
                 return;
        
             if (!isMoving)
                 return;

             transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

             if (transform.position.x < -10f)
             {
                 Destroy(gameObject);
             }
         }
}
