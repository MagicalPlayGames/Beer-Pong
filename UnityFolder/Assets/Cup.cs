using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    public bool cupInPosition = false;
    public CupScore score;
    public Material emptyMat;
    public Material selectedEmptyMat;
    public Material filledMat;
    public Material selectedFilledMat;
    public Transform[] toHide;
    public int cupsLeftToPlace;
    public bool setup;
    // Start is called before the first frame update
    void Start()
    {
        if (!cupInPosition)
        {
            GetComponent<Renderer>().material = emptyMat;
        }
        toHide = transform.GetComponentsInChildren<Transform>();
        foreach (Transform obj in toHide)
        {
            if (obj != this.transform)
                obj.gameObject.SetActive(cupInPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        setup = score.setUp;
    }
    private void OnMouseEnter()
    {
        if (setup)
        {
            if (!cupInPosition && score.cupsLeftToPlace > 0)
                GetComponent<Renderer>().material = selectedEmptyMat;
            else if (cupInPosition)
                GetComponent<Renderer>().material = selectedFilledMat;
        }
    }

    private void OnMouseExit()
    {
        if (!cupInPosition)
            GetComponent<Renderer>().material = emptyMat;
        else
            GetComponent<Renderer>().material = filledMat;
    }

    private void OnMouseDown()
    {
        if (setup)
        {
            if (cupInPosition)
            {
                cupInPosition = false;
                GetComponent<Renderer>().material = emptyMat;
                score.cupsLeftToPlace++;
            }
            else if (score.cupsLeftToPlace > 0)
            {
                cupInPosition = true;
                GetComponent<Renderer>().material = filledMat;
                score.cupsLeftToPlace--;
            }
            foreach (Transform obj in toHide)
            {
                if (obj != this.transform)
                    obj.gameObject.SetActive(cupInPosition);
            }
        }
    }

    public void scored()
    {
        cupInPosition = false;
        GetComponent<Renderer>().material = emptyMat;
        foreach (Transform obj in toHide)
        {
                obj.gameObject.SetActive(cupInPosition);
        }
    }
}
