using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour
{

    //public string portName;

    [Header("Upgrade options")]
    public UpgradeType upgrade;
    public int upgradeAmount;
    [SerializeField] public List<UpgradeChance> upgradeList = new List<UpgradeChance>();

    private GameManager gm;
    private Player p;
    private Cargo pCargo;
    private Transform circleTransform;


    [System.Serializable]
    public struct UpgradeChance
    {
        public UpgradeType type;
        public int chance;
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        p = Player.Instance;
        pCargo = p.GetComponent<Cargo>();

        upgrade = RandomUpgradeType();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Dictionary<FishType, int> cargo = pCargo.FishCargo;
        List<FishType> keys = new List<FishType>(cargo.Keys);
        foreach (var fish in keys)
        {
            Sell(fish, cargo[fish]);
        }
    }

    public UpgradeType RandomUpgradeType()
    {
        int rand = Random.Range(0, 101);
        int j = 0;
        UpgradeType upgrade = 0;
        for (int i = 0; i < upgradeList.Count; i++)
        {
            if (rand >= j && rand <= j + upgradeList[i].chance)
            {
                upgrade = upgradeList[i].type;
                break;
            }
            else
            {
                j += upgradeList[i].chance;
            }
        }
        return upgrade;
    }

    public void SellAll()
    {
        for (int i = 0; i < gm.GlobalFish.Count; i++)
        {
            FishType fishType = gm.GlobalFish[i].type;
            int fishCount = pCargo.FishCargo[fishType];

            //Get money for the fish and remove it from cargo
            p.money += fishCount * gm.GlobalFish[i].price * p.sellModifier;
            pCargo.RemoveFish(fishType, fishCount);
        }
    }

    public void Sell(FishType type, int num)
    {
        //Find the correct fish type
        for (int i = 0; i < gm.GlobalFish.Count; i++)
        {
            if (gm.GlobalFish[i].type == type)
            {
                //Get money for the fish and remove it from cargo
                p.money += num * gm.GlobalFish[i].price * p.sellModifier;
                pCargo.RemoveFish(type, num);
                break;
            }
        }
    }

    //Upgrade is randomly selected in port from upgradeList
    public void BuyRandom()
    {
        BuySpecific(upgrade, upgradeAmount);
    }

    public void BuySpecific(UpgradeType upgradeToBuy, int _upgradeAmount)
    {
        for (int i = 0; i < gm.GlobalUpgrade.Count; i++)
        {
            //Find the right upgrade and check if we have the money to buy it
            if (gm.GlobalUpgrade[i].upgrade == upgradeToBuy)
            {
                if (p.money >= gm.GlobalUpgrade[i].price)
                {
                    p.Upgrade(upgradeToBuy, _upgradeAmount);
                    p.money -= gm.GlobalUpgrade[i].price * (1 / p.buyModifier);

                    //Increase global upgrade price
                    gm.GlobalUpgrade[i].price = Mathf.RoundToInt(gm.GlobalUpgrade[i].price * gm.changePerBuy);
                }
            }
        }
    }
}
