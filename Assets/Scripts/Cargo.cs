using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cargo : MonoBehaviour
{
    public int spaceFilled;
    public int capacity;
    public Dictionary<FishType, int> FishCargo = new Dictionary<FishType, int>();

    void Update()
    {
        //Dictionaries are not shown in Unity Inspector so this is a temporary solution
        //An UI that shows different fish and their number is needed
        foreach (var key in FishCargo.Keys)
        {
            Debug.Log(key + " " + FishCargo[key]);
        }

    }

    //TODO stop catching fish if it
    public void CatchFish(FishType typeCatched, int numCatched) 
    {
        if (spaceFilled + numCatched <= capacity)
        {
            if (FishCargo.ContainsKey(typeCatched))
            {
                FishCargo[typeCatched] += numCatched;
            }
            else
            {
                FishCargo.Add(typeCatched, numCatched);
            }

            spaceFilled += numCatched;
        }
        /*
        else
        {
            capacity full, decide what to do...
        }
        */
    }
}
