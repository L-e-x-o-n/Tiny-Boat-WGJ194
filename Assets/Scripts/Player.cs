//Lexon Test
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public int money;
    public TMPro.TMP_Text moneyText;

    [Header("Modifiers")]
    public float catchRateModifier = 1;
    public float forwardSpeedModifier = 1;
    public float rotationSpeedModifier = 1;
    public int sellModifier = 1;
    public int buyModifier = 1;

    private Cargo cargo;

    // Start is called before the first frame update
    void Start()
    {
        cargo = GetComponent<Cargo>();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$" + money.ToString();
    }

    public void Upgrade(Upgrades upgrade, int amount)
    {
        switch (upgrade)
        {
            case Upgrades.Capacity:
                cargo.capacity += amount;
                break;
            case Upgrades.Speed:
                forwardSpeedModifier += amount;
                rotationSpeedModifier += amount;
                break;
            case Upgrades.CatchRate:
                catchRateModifier += amount;
                break;
            case Upgrades.FishSellPrice:
                sellModifier += amount;
                break;
            case Upgrades.UpgradeBuyPrice:
                buyModifier += amount;
                break;
            default:
                break;
        }
    }
}
