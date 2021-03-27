using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FishCircle : MonoBehaviour
{
    public FishType fishType;

    public float maxRadius;
    private float radius;

    public int numOfFish;
    public int maxFish;

    private Fish fish;
    private CircleCollider2D circleCollider;
    private float nextCatch;

    // Start is called before the first frame update
    void Start()
    {
        numOfFish = maxFish;
        circleCollider = GetComponent<CircleCollider2D>();

        //Find the correct fish struct by FishType
        foreach (var fishItem in FishManager.Instance.Fish)
        {
            if (fishItem.type == fishType)
            {
                fish = fishItem;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Less fish = smaller circle
        radius = maxRadius * ((float)numOfFish / (float)maxFish);
        Debug.Log(maxRadius * ((float)numOfFish / (float)maxFish));

        circleCollider.radius = radius;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Cargo playerCargo = collision.GetComponent<Cargo>();

            //Timer, runs evey second
            if (Time.time > nextCatch)
            {
                nextCatch = Time.time + 1;

                if (numOfFish > fish.catchPerSecond)
                {
                    playerCargo.CatchFish(fish.type, fish.catchPerSecond);
                    numOfFish -= fish.catchPerSecond;
                }
                else
                {
                    playerCargo.CatchFish(fish.type, numOfFish);
                    numOfFish = 0;
                }
            }
        }
    }
}
