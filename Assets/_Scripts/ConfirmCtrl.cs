using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ConfirmCtrl : MonoBehaviour
{
    [SerializeField] private Button confirmBtn;
    [SerializeField] private Button canleBtn;
    [SerializeField] private TextMeshProUGUI textConfirm;

    [SerializeField] public ShopCtrl shopCtrl;

    public ShopCtrl.ProductId productType;
    public int index = -1;

    private void Start()
    {
        canleBtn.onClick.AddListener(()=>transform.gameObject.SetActive(false));
        confirmBtn.onClick.AddListener(()=>
        {
            shopCtrl.BuyProduct(productType, index);
            transform.gameObject.SetActive(false);
        });
       
    }

    public void GrayConfirm()
    {
        confirmBtn.image.color=Color.gray;
        confirmBtn.gameObject.SetActive(false);
        textConfirm.text = "Bạn không đủ tiền để mua!";
    }

    public void OKConfirm()
    {
        confirmBtn.image.color=Color.white;
        confirmBtn.gameObject.SetActive(true);
        textConfirm.text = "Bạn có muốn mua?";
    }
}
