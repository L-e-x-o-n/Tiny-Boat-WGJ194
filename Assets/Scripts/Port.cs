using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour
{
    [Header("Port options")]
    public float accessRadius = 3;
    //public string portName;

    /*[Header("Economy")]
    public int maxPriceChange;
    public float scanRange;*/
    public List<Fish> LocalFish = new List<Fish>();

    private GameManager gm;
    private Player p;
    private Cargo pCargo;
    private Transform circleTransform;
    private Transform portUIParent;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        p = Player.Instance;
        pCargo = p.GetComponent<Cargo>();
        circleTransform = transform.Find("PortCircleCollider");
        circleTransform.localScale = new Vector3(accessRadius, accessRadius);

        portUIParent = transform.Find("Port UI");
        if (portUIParent == null)
        {
            Debug.LogWarning(">>Port UI<< parent missing on " + transform.name);
        }

        for (int i = 0; i < gm.GlobalFish.Count; i++)
        {
            LocalFish.Add(gm.GlobalFish[i]);
        }

        GetGlobalPrices();

        //Randomise prices
        /*for (int i = 0; i < LocalFish.Count; i++)
        {
            LocalFish[i].price += Random.Range(-maxPriceChange, maxPriceChange + 1);
        }

        //Scans for fish groups in range and decreases the price of that fish
        Collider2D[] overlaps = Physics2D.OverlapCircleAll(transform.position, scanRange);
        for (int i = 0; i < overlaps.Length; i++)
        {
            if (overlaps[i].CompareTag("FishGroup"))
            {
                foreach (var fish in LocalFish)
                {
                    if (fish.type == overlaps[i].GetComponent<FishCircle>().fishList[0].type)
                    {
                        fish.price -= Random.Range(0, maxPriceChange + 1);
                    }
                }
            }
        }

        //Check that the fish price isn't negative
        for (int i = 0; i < LocalFish.Count; i++)
        {
            if (LocalFish[i].price <= 0)
            {
                LocalFish[i].price = 1;
            }
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OpenPortUI();
        Dictionary<FishType, int> cargo = pCargo.FishCargo;
        List<FishType> keys = new List<FishType>(cargo.Keys);
        foreach (var fish in keys)
        {
            Sell(fish, cargo[fish]);
        }
    }

    public void OpenPortUI()
    {

    }

    void GetGlobalPrices()
    {
        for (int i = 0; i < gm.GlobalFish.Count; i++)
        {
            for (int j = 0; j < LocalFish.Count; j++)
            {
                if (LocalFish[j].type == gm.GlobalFish[i].type)
                {
                    LocalFish[j].price = gm.GlobalFish[i].price;
                }
            }
        }

    }

    public void Sell(FishType type, int num)
    {
        //Find the correct fish type
        for (int i = 0; i < LocalFish.Count; i++)
        {
            if (LocalFish[i].type == type)
            {
                //Get money for the fish and remove it from cargo
                p.money += num * LocalFish[i].price * p.sellModifier;
                pCargo.RemoveFish(type, num);
                return;
            }
        }
    }

    public void Buy(Upgrades upgradeToBuy, int upgradeAmount)
    {
        for (int i = 0; i < gm.GlobalUpgrade.Count; i++)
        {
            if (gm.GlobalUpgrade[i].upgrade == upgradeToBuy)
            {
                p.Upgrade(upgradeToBuy, upgradeAmount);
                p.money -= gm.GlobalUpgrade[i].price * (1 /  p.buyModifier);
            }
        }
    }
}
