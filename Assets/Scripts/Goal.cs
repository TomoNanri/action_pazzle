using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D()
    {
        if (GameManager.current.hasGotGoalKey) {
            GameManager.current.hasGotGoal = true;
            Debug.Log("GOAL IN, hasGotGoal = " + GameManager.current.hasGotGoal); 
        } else
        {
            Debug.Log("Throug GOAL. hasGotGoal = " + GameManager.current.hasGotGoal);
        }
    }
}
