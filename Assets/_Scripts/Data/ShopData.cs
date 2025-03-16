
using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Shop Data", menuName = "Data/Shop Data")]
public class ShopData : ScriptableObject
{
    public List<ProductParam> SkinParams;
    public List<ProductParam> BackgroundParams;
}

[Serializable]
public class ProductParam
{
    public string Name;
    public string Cost;
    public bool IsUnlock;
    public Sprite ProductSprite;
}
