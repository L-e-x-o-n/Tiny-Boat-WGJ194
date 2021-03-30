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

    [Header("Cargo UI")]
    public bool showCargoUI = true;
    public TMPro.TMP_Text textPrefab;
    public Transform UIParent;

    private GameManager gm;

    void Start()
    {
        gm = GameManager.Instance;

        for (int i = 0; i < gm.GlobalFish.Count; i++)
        {
            FishCargo.Add(gm.GlobalFish[i].type, 0);
        }
    }

    void Update()
    {
        if (showCargoUI)
        {
            UpdateCargoUI();
        }
    }

    public void UpdateCargoUI()
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
            cargoUI[UIText].text = UIText.ToString() + ": " + FishCargo[UIText].ToString();
        }
    }


    public void CatchFish(FishType typeCatched, int numCatched) 
    {
        if (spaceFilled + numCatched <= capacity)
        {
            if (FishCargo.ContainsKey(typeCatched))
            {
                FishCargo[typeCatched] += numCatched;
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

    public void RemoveFish(FishType type, int num)
    {
        if (num >= FishCargo[type])
        {
            FishCargo[type] -= num;
            spaceFilled -= num;
        }
        else
        {
            Debug.LogWarning("Trying to remove more fish than there is in cargo.");
        }
    }
}
