using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public Upgrades upgrade;
    private Port port;
    // Start is called before the first frame update
    void Start()
    {
        port = gameObject.GetComponentInParent<Port>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player Boat")
        {
            port.Buy(upgrade, 1);
            Destroy(gameObject);
        }
    }
}
