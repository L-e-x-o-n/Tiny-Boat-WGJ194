using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortDetector : MonoBehaviour
{
    public Action action;

    private Port port;
    private Transform t;

    // Start is called before the first frame update
    void Start()
    {
        port = gameObject.GetComponentInParent<Port>();
        t = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (action == Action.Sell)
            {
                port.SellAll();
            }
            else if (action == Action.Upgrade)
            {
                port.BuyRandom();
            }
        }
    }

    public enum Action
    {
        Sell = 0,
        Upgrade = 1,
    }
}
