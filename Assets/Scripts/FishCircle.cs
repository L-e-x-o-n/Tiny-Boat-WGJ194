using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCircle : MonoBehaviour
{
    //A list of fishes that can be caught and chances to catch them. The sum of all chances should be 100
    [SerializeField]public List<FishChance> fishList = new List<FishChance>();

    [Tooltip("Starting and max range in which fish can be caught.")]
    public float maxRadius;
    private float radius; //Decreases as fish are caught

    [Tooltip("Max number of fish that can be catched from this object")]
    public int maxFish;
    private int numOfFish; //Current number of fish that can be caught

    [Tooltip("Time in seconds between each catch")]
    public float catchDelay = 1;

    [Tooltip("After catch delay this is the max number of fish that can be caught.")]
    public int maxFishPerCatch = 1;

    private CircleCollider2D circleCollider;
    private float nextCatch;
    private Transform borderCircle;

    [System.Serializable]
    public struct FishChance
    {
        public FishType type;
        public int chance;
    }

    // Start is called before the first frame update
    void Start()
    {
        numOfFish = maxFish;

        circleCollider = GetComponent<CircleCollider2D>();
        borderCircle = transform.Find("BorderCircle");
    }

    // Update is called once per frame
    void Update()
    {
        //Less fish = smaller circle
        radius = maxRadius * ((float)numOfFish / (float)maxFish);

        circleCollider.radius = radius;
        borderCircle.localScale = new Vector2(radius, radius);
    }

    public FishType RandomFishType()
    {
        int rand = Random.Range(0, 101);
        int j = 0;
        FishType fishToDrop = 0;
        for (int i = 0; i < fishList.Count; i++)
        {
            if (rand >= j && rand <= j + fishList[i].chance)
            {
                fishToDrop = fishList[i].type;
                break;
            }
            else
            {
                j += fishList[i].chance;
            }
        }
        return fishToDrop;
    }

    //Called every once per frame if the player is touching the colider/trigger
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && numOfFish > 0)
        {
            Cargo playerCargo = collision.GetComponent<Cargo>();

            //Timer, runs every catchDelay seconds
            if (Time.time > nextCatch)
            {
                nextCatch = Time.time + catchDelay;
                int catchNum = Random.Range(1, Mathf.Min(maxFishPerCatch, numOfFish)+ 1);

                if (numOfFish > catchNum)
                {
                    playerCargo.CatchFish(RandomFishType(), catchNum);
                    numOfFish -= catchNum;
                }
            }
        }
    }
}
