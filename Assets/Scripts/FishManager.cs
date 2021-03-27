using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FishManager : Singleton<FishManager>
{
    //A list of fish, editable in the inspector
    public List<Fish> GlobalFish = new List<Fish>();
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

[Serializable]
public class Fish
{
    public FishType type;
    public int price;
}
