using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonOnClick : MonoBehaviour
{
    public Button skinBtn;
    public Button backgroundBtn;
    
    public Color defaultColor = Color.white;
    public Color selectedColor = Color.yellow;
    // Start is called before the first frame update
    void Start()
    {
        SetButtonColor(skinBtn, defaultColor);
        SetButtonColor(backgroundBtn, defaultColor);
        
        skinBtn.onClick.AddListener(OnSkinBtnClick);
        backgroundBtn.onClick.AddListener(OnBackgroundBtnClick);
        OnSkinBtnClick();
    }

    // Update is called once per frame
    public void OnSkinBtnClick()
    {
        
        SetButtonColor(skinBtn, selectedColor);
        SetButtonColor(backgroundBtn, defaultColor);
        
    }

    public void OnBackgroundBtnClick()
    {
        SetButtonColor(skinBtn, defaultColor);
        SetButtonColor(backgroundBtn, selectedColor);
    }

    void SetButtonColor(Button button, Color color)
    {
        
        ColorBlock colors = button.colors;
        colors.normalColor = color;
        colors.highlightedColor = color;
        colors.pressedColor = color;
        colors.selectedColor = color;
        button.colors = colors;
        
    }
}
