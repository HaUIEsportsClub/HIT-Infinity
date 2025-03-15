using UnityEngine;


public class Letter : MonoBehaviour
{
    public Sprite m_LetterSprite;
    public char m_LetterChar;

    public void SetLetter(Sprite letterSprite, char letterChar)
    {
        m_LetterSprite = letterSprite;
        m_LetterChar = letterChar;
    }

    public void StopMoving()
    {
        MovingItem movingItem = GetComponent<MovingItem>();
        if (movingItem != null)
        {
            movingItem.StopMovement();
        }
    }
}