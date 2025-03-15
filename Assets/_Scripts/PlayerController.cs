using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isGrounded = true;
    
    public int health = 5;
    public int maxHealth = 5;
    public float rotationSpeed = -250f;
   
    void Awake()
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
        rb = GetComponent<Rigidbody2D>();
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateHealthUI(health);
        }
        
    }
    void Update()
    {
        if (GameManager.Instance.gameActive == false) return;
        if (isGrounded && (Input.GetKeyDown(KeyCode.Space) ))
        {
            rb.velocity = Vector2.up * jumpForce;
            isGrounded = false;
        }

            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Letter"))
        {
            GameManager.Instance.CheckLetter(other.gameObject);
        }
        else if (other.CompareTag("Gold"))
        {
            if (GoldManager.Instance != null)
            {
                GoldManager.Instance.AddGold(10);
                Debug.Log("add gold");
            }

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Health"))
        {
            if (health < maxHealth)
            {
                health++;
                if (UIManager.Instance != null)
                {
                    UIManager.Instance.UpdateHealthUI(health);
                }
                Destroy(other.gameObject);
            }
        }
    }

    public void TakeDamage()
    {
        health--;
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateHealthUI(health);
        }

        if (health <= 0)
        {
            GameManager.Instance.gameActive = false;
            UIManager.Instance.gameOverPanel.SetActive(true);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

}