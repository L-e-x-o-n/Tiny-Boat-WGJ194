//Lexon Test
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    //A list of fish types and their prices, editable in the inspector
    public List<Fish> GlobalFish = new List<Fish>();

    //A list of upgrades and their prices, editable in the inspector
    public List<UpgradePrice> GlobalUpgrade = new List<UpgradePrice>();
}

public enum FishType
{
    Red,
    Orange,
    Yellow,
    Blue,
    Tuna,
    UltraRare,
}

public enum Upgrades
{
    Capacity,
    Speed,
    CatchRate,
    FishSellPrice,
    UpgradeBuyPrice,
}

[Serializable]
public class Fish
{
    public FishType type;
    public int price;
}

[Serializable]
public class UpgradePrice
{
    public Upgrades upgrade;
    public int price;
}
