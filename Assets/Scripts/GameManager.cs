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

    //"How much the price changes for each upgrade bought. <0 gets cheaper >0 get more expensive
    public float changePerBuy = 1.5f;

    public GameObject quitUI;

    #region UI Functions
    public void QuitGame()
    {
        Application.Quit();
    }

    public void QuitMenu(bool open)
    {
        if (open)
        {
            quitUI.SetActive(true);
        }
        else
        {
            quitUI.SetActive(false);
        }
    }

    public void Maybe()
    {
        if (UnityEngine.Random.Range(0,2) == 1)
        {
            Application.Quit();
        }
        else
        {
            quitUI.SetActive(false);
        }
    }

    #endregion

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (quitUI.activeInHierarchy)
            {
                quitUI.SetActive(false);
            }
            else
            {
                quitUI.SetActive(true);
            }
        }
    }
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

public enum UpgradeType
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
    public UpgradeType upgrade;
    public int price;
}
