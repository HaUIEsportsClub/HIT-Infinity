using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ConfirmCtrl : MonoBehaviour
{
    [SerializeField] private Button confirmBtn;
    [SerializeField] private Button canleBtn;

    [SerializeField] public ShopCtrl shopCtrl;

    public ShopCtrl.ProductId productType;
    public int index = -1;

    private void Start()
    {
        canleBtn.onClick.AddListener(()=>transform.gameObject.SetActive(false));
        confirmBtn.onClick.AddListener(()=> shopCtrl.BuyProduct(productType,index));
    }
}
