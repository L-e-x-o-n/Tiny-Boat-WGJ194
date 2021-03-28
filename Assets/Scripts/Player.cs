using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public int money;
    public TMPro.TMP_Text moneyText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$" + money.ToString();
    }

    class Upgrade
    {
        int cost;
        int capcaity; 
    }
}
