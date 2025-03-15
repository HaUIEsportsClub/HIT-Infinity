using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    private Sprite m_LetterSprite;
    private char m_LetterChar;
    

    public void SetLetter(Sprite letterSprite, char letterChar)
    {
        m_LetterSprite = letterSprite;
        m_LetterChar = letterChar;
    }
}
