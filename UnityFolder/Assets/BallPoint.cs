using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public CupScore score;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.name.Contains("Ball") && GetComponentInParent<Cup>().cupInPosition)
        {
            StartCoroutine(changeState(collision.gameObject));
            score.updateScore();
        }
    }
    IEnumerator changeState(GameObject ball)
    {
        yield return new WaitForSeconds(2);
        GetComponentInParent<Cup>().scored();
        Destroy(ball);

    }
}
