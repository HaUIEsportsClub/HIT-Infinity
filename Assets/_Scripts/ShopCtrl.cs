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
        public bool isUnlock;
    }

    [SerializeField] private List<ProductInfor> m_Skins;
    [SerializeField] private List<ProductInfor> m_Backgrounds;

    [SerializeField] private ConfirmCtrl m_ConfirmCtrl;

    public void Start()
    {
        for (int i = 0; i < m_Skins.Count; ++i)
        {
            m_Skins[i].productBtn.onClick.AddListener(()=>ClickSkin(i));
        }

        for (int i = 0; i < m_Backgrounds.Count; ++i)
        {
            m_Backgrounds[i].productBtn.onClick.AddListener(()=>ClickBackGround(i));
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
        ChooseSkin();
    }

    private void BuyBackground(int groundId)
    {
        m_Backgrounds[groundId].isUnlock = true;
        ChooseBackground();
    }

    private void ChooseSkin()
    {
        
    }

    private void ChooseBackground()
    {
        
    }
    
}
