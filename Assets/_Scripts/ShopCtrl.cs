using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopCtrl : MonoBehaviour
{
    public enum ProductId
    {
        Skin,
        Background
    }
    [System.Serializable]
    private class ProductInfor
    {
        public Button productBtn;
        public TextMeshProUGUI productText;
        public Image productImg;
        public bool isUnlock;
    }

    [Header("__________Data__________")] 
    [SerializeField] private ShopData m_ShopData;
    [Header("__________Confirm__________")]
    [SerializeField] private ConfirmCtrl m_ConfirmCtrl;
    [Header("__________Holder__________")]
    [SerializeField] private List<ProductInfor> m_Skins;
    [SerializeField] private List<ProductInfor> m_Backgrounds;
    
    

    public void Start()
    {
        
        FirstLoadSkin();
        FirstLoadBackground();
        
        
    }

    private void FirstLoadSkin()
    {
        List<ProductParam> skinParams = m_ShopData.SkinParams;
        
        for (int i = 0; i < skinParams.Count; ++i)
        {
            if(i >= m_Skins.Count) return;
            m_Skins[i].productText.text = skinParams[i].Cost;
            m_Skins[i].isUnlock = skinParams[i].IsUnlock;
            m_Skins[i].productImg.sprite = skinParams[i].ProductSprite;
            var i1 = i;
            m_Skins[i].productBtn.onClick.AddListener(()=>ClickSkin(i1));
            
        }
    }

    private void FirstLoadBackground()
    {
        List<ProductParam> backgroundParams = m_ShopData.BackgroundParams;
        for (int i = 0; i < backgroundParams.Count; ++i)
        {
            if(i > m_Backgrounds.Count) return;
            m_Backgrounds[i].productText.text = backgroundParams[i].Cost;
            m_Backgrounds[i].isUnlock = backgroundParams[i].IsUnlock;
            m_Backgrounds[i].productImg.sprite =backgroundParams[i].ProductSprite;
            var i1 = i;
            m_Backgrounds[i].productBtn.onClick.AddListener(()=>ClickBackGround(i1));
           
        }
    }
    private void ClickSkin(int skinId)
    {
        if (m_Skins[skinId].isUnlock) ChooseSkin();
        else
        {
            if (m_ConfirmCtrl != null)
            {
                m_ConfirmCtrl.gameObject.SetActive(true);
                m_ConfirmCtrl.productType = ProductId.Skin;
                m_ConfirmCtrl.index = skinId;
            }
        }
    }

    private void ClickBackGround(int backGroundId)
    {
        if (m_Backgrounds[backGroundId].isUnlock) ChooseBackground();
        else
        {
            if (m_ConfirmCtrl != null)
            {
                m_ConfirmCtrl.gameObject.SetActive(true);
                m_ConfirmCtrl.productType = ProductId.Background;
                m_ConfirmCtrl.index = backGroundId;
            }
        }
    }

    public void BuyProduct(ProductId productId, int index)
    {
        if (index == -1)
        {
            if(m_ConfirmCtrl != null) m_ConfirmCtrl.gameObject.SetActive(false);
            return;
        }

        switch (productId)
        {
            case ProductId.Skin:
                BuySkin(index);
                break;
            case ProductId.Background:
                BuyBackground(index);
                break;
        }
    }

    private void BuySkin(int skinId)
    {
        m_Skins[skinId].isUnlock = true;
        PlayerPrefs.SetInt("Skin_" + skinId + "_unlock",1);
        PlayerPrefs.Save();
        ChooseSkin();
    }

    private void BuyBackground(int groundId)
    {
        m_Backgrounds[groundId].isUnlock = true;
        PlayerPrefs.SetInt("Background_" + groundId + "unlock",1);
        PlayerPrefs.Save();
        ChooseBackground();
    }

    private void ChooseSkin()
    {
        
    }

    private void ChooseBackground()
    {
        
    }

    public void UpdateSkinShop()
    {
        for (int i = 0; i < m_Skins.Count; ++i)
        {
            int unlock = PlayerPrefs.GetInt("Skin_" + i + "_unlock", 0);
            if (unlock == 0)
            {
                //set lock
            }
            else
            {
                //set unlock
            }
        }
    }

    public void UpdateBackgroundShop()
    {
        for (int i = 0; i < m_Backgrounds.Count; ++i)
        {
            int unlock = PlayerPrefs.GetInt("Background_" + i + "unlock",0);
            if (unlock == 0)
            {
                //set lock
            }
            else
            {
                //set unlock
            }
        }
    }
    
}
