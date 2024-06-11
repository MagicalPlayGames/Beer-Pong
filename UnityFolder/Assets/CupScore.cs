using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupScore : MonoBehaviour
{
    public int count;
    public int cupsLeftToPlace;
    public Cup[] cupScripts;
    public bool setUp;
    void Start()
    {
        setUp = false;
        cupScripts = GetComponentsInChildren<Cup>();
        cupsLeftToPlace = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateScore()
    {
        count++;
    }

    public void disableSetup()
    {
        setUp = false;
        foreach(Cup curCup in cupScripts)
        {
            curCup.setup = false;
            curCup.enabled = false;
            if(!curCup.cupInPosition)
            {
                curCup.gameObject.SetActive(false);
            }
        }
    }

    public void startSetup()
    {
        setUp = true;
        foreach (Cup curCup in cupScripts)
        {
            curCup.enabled = true;
            if (!curCup.cupInPosition)
            {
                curCup.gameObject.SetActive(true);
            }
        }
    }
}
