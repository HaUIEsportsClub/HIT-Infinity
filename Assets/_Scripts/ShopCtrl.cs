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
        public Image lockImg;
        public bool isUnlock;
    }

    [Header("__________Data__________")] 
    [SerializeField] private ShopData m_ShopData;
    [Header("__________Confirm__________")]
    [SerializeField] private ConfirmCtrl m_ConfirmCtrl;
    [Header("__________Holder__________")]
    [SerializeField] private List<ProductInfor> m_Skins;
    [SerializeField] private List<ProductInfor> m_Backgrounds;

    private void Reset()
    {
        LoadComponent();
    }

    private void Awake()
    {
        LoadComponent();
    }

    public void Start()
    {
        
        FirstLoadSkin();
        FirstLoadBackground();
        
        
    }

    private void FirstLoadSkin()
    {
        if(m_ShopData == null) return;
        List<ProductParam> skinParams = m_ShopData.SkinParams;
        
        for (int i = 0; i < skinParams.Count; ++i)
        {
            if(i >= m_Skins.Count) return;
            m_Skins[i].productText.text = skinParams[i].Cost;
            m_Skins[i].isUnlock = skinParams[i].IsUnlock;
            if(m_Skins[i].isUnlock) m_Skins[i].lockImg.gameObject.SetActive(false);
            else m_Skins[i].lockImg.gameObject.SetActive(true);
            m_Skins[i].productImg.sprite = skinParams[i].ProductSprite;
            var i1 = i;
            m_Skins[i].productBtn.onClick.AddListener(()=>ClickSkin(i1));
            
        }
    }

    private void FirstLoadBackground()
    {
        if(m_ShopData == null) return;
        List<ProductParam> backgroundParams = m_ShopData.BackgroundParams;
        for (int i = 0; i < backgroundParams.Count; ++i)
        {
            if(i > m_Backgrounds.Count) return;
            m_Backgrounds[i].productText.text = backgroundParams[i].Cost;
            m_Backgrounds[i].isUnlock = backgroundParams[i].IsUnlock;
            if(m_Backgrounds[i].isUnlock) m_Backgrounds[i].lockImg.gameObject.SetActive(false);
            else m_Backgrounds[i].lockImg.gameObject.SetActive(true);
            m_Backgrounds[i].productImg.sprite =backgroundParams[i].ProductSprite;
            var i1 = i;
            m_Backgrounds[i].productBtn.onClick.AddListener(()=>ClickBackGround(i1));
           
        }
    }

    private void LoadComponent()
    {
        
        
        if (m_Skins == null) m_Skins = new List<ProductInfor>();
        if (m_Skins.Count <= 0)
        {
            Transform skinHolder = transform.Find("Skin_Shop").Find("Viewport").Find("Content");
            foreach (Transform skinItem in skinHolder)
            {
                m_Skins.Add(new ProductInfor()
                {
                    productBtn = skinItem.GetComponentInChildren<Button>(),
                    productText = skinItem.GetComponentInChildren<TextMeshProUGUI>(),
                    productImg = skinItem.Find("skin_image").GetComponent<Image>(),
                    lockImg = skinItem.Find("lock").GetComponent<Image>()
                });
            }
        }
        

       
        if (m_Backgrounds == null) m_Backgrounds = new List<ProductInfor>();
        
        if (m_Backgrounds.Count <= 0)
        {
            Transform backgroundHolder = transform.Find("BG_Shop").Find("Viewport").Find("Content");
            foreach (Transform backgroundItem in backgroundHolder)
            {
                m_Backgrounds.Add(new ProductInfor()
                {
                    productBtn = backgroundItem.GetComponentInChildren<Button>(),
                    productText = backgroundItem.GetComponentInChildren<TextMeshProUGUI>(),
                    productImg = backgroundItem.Find("skin_image").GetComponent<Image>(),
                    lockImg = backgroundItem.Find("lock").GetComponent<Image>()
                });
            }
            transform.Find("BG_Shop").gameObject.SetActive(false);
        }
        
    }
    private void ClickSkin(int skinId)
    {
        if (m_Skins[skinId].isUnlock) ChooseSkin(skinId);
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
        if (m_Backgrounds[backGroundId].isUnlock) ChooseBackground(backGroundId);
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
        m_Skins[skinId].lockImg.gameObject.SetActive(false);
        m_Skins[skinId].productText.gameObject.SetActive(false);
        PlayerPrefs.SetInt("Skin_" + skinId + "_unlock",1);
        PlayerPrefs.Save();
        ChooseSkin(skinId);
    }

    private void BuyBackground(int groundId)
    {
        m_Backgrounds[groundId].isUnlock = true;
        m_Backgrounds[groundId].lockImg.gameObject.SetActive(false);
        m_Backgrounds[groundId].productText.gameObject.SetActive(false);
        PlayerPrefs.SetInt("Background_" + groundId + "unlock",1);
        PlayerPrefs.Save();
        ChooseBackground(groundId);
    }

    private void ChooseSkin(int skinId)
    {
        GameManager.Instance.SkinId = skinId;
        //active UI choose skin
    }

    private void ChooseBackground(int groundId)
    {
        GameManager.Instance.BackgroundId = groundId;
        //active UI choose background
    }

    public void UpdateSkinShop()
    {
        for (int i = 0; i < m_Skins.Count; ++i)
        {
            int unlock = PlayerPrefs.GetInt("Skin_" + i + "_unlock", 0);
            if (unlock == 0)
            {
                m_Skins[i].lockImg.gameObject.SetActive(true);
            }
            else
            {
                m_Skins[i].lockImg.gameObject.SetActive(false);
                m_Skins[i].productText.gameObject.SetActive(false);
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
                m_Backgrounds[i].lockImg.gameObject.SetActive(true);
            }
            else
            {
                m_Backgrounds[i].lockImg.gameObject.SetActive(false);
                m_Backgrounds[i].productText.gameObject.SetActive(false);
            }
        }
    }
    
}
