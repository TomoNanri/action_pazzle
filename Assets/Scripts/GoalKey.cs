using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKey : MonoBehaviour
{
    private bool blink = false;
    public bool hidden = false;
    private Color baseColor;
    private float timer = 0.0f;
    private int blinkCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        baseColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.current.hasGotGoalKey && !hidden)
        {
            timer += Time.deltaTime;
            if (timer >= 0.2f)
            {
                timer = 0.0f;
                if (blinkCount++ > 8)
                {
                    hidden = true;
                    GetComponent<SpriteRenderer>().color += new Color(-0.7f, -0.7f, -0.7f);
                }
                else
                {
                    if (blink)
                    {
                        blink = false;
                        GetComponent<SpriteRenderer>().color += new Color(-0.6f, -0.6f, -0.6f);
                    }
                    else
                    {
                        blink = true;
                        GetComponent<SpriteRenderer>().color = baseColor;
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D()
    {
        GameManager.current.hasGotGoalKey = true;
        Debug.Log("Goal Key = " + GameManager.current.hasGotGoalKey);
    }
}
