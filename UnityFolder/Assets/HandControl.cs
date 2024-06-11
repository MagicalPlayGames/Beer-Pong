using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControl : MonoBehaviour
{
    public enum stage {Throw,SetUp,Waiting};
    public stage curStage;
    public Throwing throwScript;
    public CupScore cupScoreScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (curStage == stage.Throw)
        {
            throwScript.throwingLoop();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (curStage == stage.Throw)
            {
                curStage++;
                cupScoreScript.startSetup();
            }
            else if (curStage == stage.SetUp && cupScoreScript.cupsLeftToPlace<=0)
            {
                curStage--;
                cupScoreScript.disableSetup();
            }
        }
    }
}
