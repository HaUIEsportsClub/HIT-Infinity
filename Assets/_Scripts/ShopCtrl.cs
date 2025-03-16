using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private GameObject m_TickPrefab;
    [SerializeField] private ConfirmCtrl m_ConfirmCtrl;

    [Header("__________Exit_____________")] [SerializeField]
    private Button m_ExitBtn;

    [Header("__________Coin_____________")] [SerializeField]
    private TextMeshProUGUI m_CoinTxt;
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
        
        bool firstLoad = (PlayerPrefs.GetInt("FirstLoadShop", 1) == 1);
        if(firstLoad) PlayerPrefs.SetInt("FirstLoadShop",0);
        FirstLoadSkin(firstLoad);
        FirstLoadBackground(firstLoad);
        UpdateSkinShop();

        if (GoldManager.Instance != null && m_CoinTxt != null) m_CoinTxt.text = GoldManager.Instance.gold.ToString(); 
       
        m_ExitBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("LevelSelection");
        });
        
    }

    private void FirstLoadSkin(bool firstLoad)
    {
        if(m_ShopData == null) return;
        List<ProductParam> skinParams = m_ShopData.SkinParams;
        
        for (int i = 0; i < skinParams.Count; ++i)
        {
            if(i >= m_Skins.Count) return;
            
            m_Skins[i].productText.text = skinParams[i].Cost;
            m_Skins[i].isUnlock = skinParams[i].IsUnlock;
            if(m_Skins[i].isUnlock){ m_Skins[i].lockImg.gameObject.SetActive(false);}
            else m_Skins[i].lockImg.gameObject.SetActive(true);

            if (firstLoad)
            {
                int valueSetted = 0;
                if (m_Skins[i].isUnlock) valueSetted = 1;
                else valueSetted = 0;
                PlayerPrefs.SetInt("Skin_" + i + "_unlock",valueSetted);
                PlayerPrefs.Save();
            }
            m_Skins[i].productImg.sprite = skinParams[i].ProductSprite;
            var i1 = i;
            m_Skins[i].productBtn.onClick.AddListener(()=>ClickSkin(i1));
            
        }
    }

    private void FirstLoadBackground(bool firstLoad)
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
            if (firstLoad)
            {
                int valueSetted = 0;
                if (m_Backgrounds[i].isUnlock) valueSetted = 1;
                else valueSetted = 0;
                PlayerPrefs.SetInt("Background_" + i + "unlock",valueSetted);
                PlayerPrefs.Save();
            }
            m_Backgrounds[i].productImg.sprite =backgroundParams[i].ProductSprite;
            int backgroundId = i;
            m_Backgrounds[i].productBtn.onClick.AddListener(()=>ClickBackGround(backgroundId));
           
        }
    }

    private void LoadComponent()
    {
        if (m_TickPrefab == null)
        {
            m_TickPrefab = transform.Find("Tick").gameObject;
            m_TickPrefab.SetActive(false);
        }

        if (m_CoinTxt == null) m_CoinTxt = transform.Find("Gold").GetComponentInChildren<TextMeshProUGUI>();
        
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

        if (m_ExitBtn == null) m_ExitBtn = transform.Find("CLOSE").GetComponent<Button>();
        if (m_ConfirmCtrl == null)
        {
            m_ConfirmCtrl = transform.GetComponentInChildren<ConfirmCtrl>();
            m_ConfirmCtrl.gameObject.SetActive(false);
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
        int unlock = PlayerPrefs.GetInt("Skin_" + skinId + "_unlock", 0);
        if (unlock == 1) ChooseSkin(skinId);
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
        int unlock = PlayerPrefs.GetInt("Background_" + backGroundId + "unlock",0);
        if (unlock == 1) ChooseBackground(backGroundId);
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
        if(!MinusCoin(int.Parse(m_ShopData.SkinParams[skinId].Cost))) return;
        m_Skins[skinId].isUnlock = true;
        m_Skins[skinId].lockImg.gameObject.SetActive(false);
        m_Skins[skinId].productText.gameObject.SetActive(false);
        PlayerPrefs.SetInt("Skin_" + skinId + "_unlock",1);
        PlayerPrefs.Save();
        ChooseSkin(skinId);
    }

    private void BuyBackground(int groundId)
    {
        if(!MinusCoin(int.Parse(m_ShopData.BackgroundParams[groundId].Cost))) return;
        m_Backgrounds[groundId].isUnlock = true;
        m_Backgrounds[groundId].lockImg.gameObject.SetActive(false);
        m_Backgrounds[groundId].productText.gameObject.SetActive(false);
        PlayerPrefs.SetInt("Background_" + groundId + "unlock",1);
        PlayerPrefs.Save();
        ChooseBackground(groundId);
    }

    private void ChooseSkin(int skinId)
    {
        PlayerPrefs.SetInt("SkinSelected",skinId);

        if (m_TickPrefab != null)
        {
            
            Transform tickHolder = m_Skins[skinId].lockImg.transform.parent;
            ShowTick(tickHolder);
            
        }
    }

    private void ChooseBackground(int groundId)
    {
        
        PlayerPrefs.SetInt("BackgroundSelected",groundId);
        if (m_TickPrefab != null)
        {
            Transform tickHolder = m_Backgrounds[groundId].lockImg.transform.parent;
            ShowTick(tickHolder);
            
        }
    }

    public void UpdateSkinShop()
    {
        int skinId = PlayerPrefs.GetInt("SkinSelected", -1);
        if(skinId >= 0) ShowTick(m_Skins[skinId].lockImg.transform.parent);
        else
        {
            if(PlayerPrefs.GetInt("Skin_" + 0 + "_unlock",0) == 1) ShowTick(m_Skins[0].lockImg.transform.parent);
        }
        for (int i = 0; i < m_Skins.Count; ++i)
        {
            int unlock = PlayerPrefs.GetInt("Skin_" + i + "_unlock", 0);
            if (unlock == 0)
            {
                m_Skins[i].lockImg.gameObject.SetActive(true);
                m_Skins[i].productText.gameObject.SetActive(true);
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
        int groundSelected = PlayerPrefs.GetInt("BackgroundSelected", -1);
        
        if(groundSelected >= 0) ShowTick(m_Backgrounds[groundSelected].lockImg.transform.parent);
        else
        {
            if(PlayerPrefs.GetInt("Background_" + 0 + "unlock",0) == 1) ShowTick(m_Backgrounds[0].lockImg.transform.parent);
        }
        for (int i = 0; i < m_Backgrounds.Count; ++i)
        {
            int unlock = PlayerPrefs.GetInt("Background_" + i + "unlock",0);
            if (unlock == 0)
            {
                m_Backgrounds[i].lockImg.gameObject.SetActive(true);
                m_Backgrounds[i].productText.gameObject.SetActive(true);
            }
            else
            {
                m_Backgrounds[i].lockImg.gameObject.SetActive(false);
                m_Backgrounds[i].productText.gameObject.SetActive(false);
            }
        }
    }

    private void ShowTick(Transform tickHolder)
    {
        
        Vector3 position = tickHolder.position;
        m_TickPrefab.transform.position = position;
        m_TickPrefab.transform.SetParent(tickHolder,true);
        m_TickPrefab.SetActive(true);
    }

    private bool MinusCoin(int amount)
    {
        if (GoldManager.Instance != null)
        {
            if (GoldManager.Instance.gold >= amount)
            {
                GoldManager.Instance.AddGold(-1 * amount);
                m_CoinTxt.text = GoldManager.Instance.gold.ToString();
                return true;
            }
            else
            {
                Debug.Log("Không đủ tiền");
                return false;
            }
        }

        return false;
    }
}
