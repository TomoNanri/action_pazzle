using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private Color baseColor;
    private Text gameOverText;
    private float timer1sec;
    private bool sw = false;

    // Start is called before the first frame update
    void Start()
    {
        timer1sec = 0;
        gameOverText = GetComponent<Text>();
        baseColor = gameOverText.color;
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    void FixedUpdate()
    {
        timer1sec += Time.deltaTime;
        if (timer1sec >= 1.0f)
        {
            timer1sec = 0.0f;
            if (sw)
            {
                sw = false;
                gameOverText.color += new Color(-0.3f, -0.3f, -0.3f);
            }
            else
            {
                sw = true;
                gameOverText.color = baseColor;
            }
        }
    }
}
