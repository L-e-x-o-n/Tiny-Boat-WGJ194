using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cargo : MonoBehaviour
{
    public int spaceFilled;
    public int capacity;
    public Dictionary<FishType, int> FishCargo = new Dictionary<FishType, int>();
    private Dictionary<FishType, TMPro.TMP_Text> cargoUI = new Dictionary<FishType, TMPro.TMP_Text>();
    public TMPro.TMP_Text textPrefab;
    public Transform UIParent;

    void Update()
    {
        //Dictionaries are not shown in Unity Inspector so this is a temporary solution
        //An UI that shows different fish and their number is needed
        int i = 0;
        foreach (var key in FishCargo.Keys)
        {
            if (cargoUI.ContainsKey(key) == false)
            {
                TMPro.TMP_Text textUi = Instantiate(textPrefab, UIParent);
                textUi.transform.position -= new Vector3(0, i * 20, 0);
                cargoUI.Add(key, textUi);
            }
            i++;
        }

        foreach (var UIText in cargoUI.Keys)
        {
            cargoUI[UIText].text = UIText.ToString() +": "+ FishCargo[UIText].ToString();
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
