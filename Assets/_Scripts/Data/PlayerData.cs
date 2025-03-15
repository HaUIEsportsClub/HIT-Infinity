
using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Data", menuName = "Data/Player Data")]
public class PlayerData : ScriptableObject
{
    public List<PlayerParam> PlayerParams;
}

[Serializable]
public class PlayerParam
{
    public string Name;
    public string Cost;
    public Sprite PlayerSprite;
}
