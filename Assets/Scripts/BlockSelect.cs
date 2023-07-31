using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public delegate void CallBack();

public class BlockSelect : MonoBehaviour
{
    private bool selected = false;
    private SpriteRenderer blkRenderer;
    private Color baseColor;
    private bool modified = false;
    private bool isBlink = false;
    private float eTime = 0;
    private bool blinkOn = false;

    // Start is called before the first frame update
    void Start()
    {
        blkRenderer = GetComponent<SpriteRenderer>();
        baseColor = blkRenderer.color;
    }

    void OnMouseEnter()
    {
        if (!modified)
        {
            selected = true;
            blkRenderer.color = new Color(0.3f, 0.3f, 0.3f);
        }
    }

    void OnMouseExit()
    {
        if (!modified)
        {
            selected = false;
            blkRenderer.color = baseColor;
        }
    }

    void OnMouseDown()
    {
        if (selected)
        {
            if (gameObject.name == "TimerBlock")
            {
                if (GameManager.current.timerBlockCount > 0) {
                    GameManager.current.timerBlockCount--;
                    modified = true;
                    Color c = baseColor;
                    c.a = 1.0f;
                    blkRenderer.color = c;
                    Debug.Log("--- Timer Block Selected! ---");
                    GetComponent<TimerBlock>().Appear(); 
                }
            }
            else
            {
                Debug.Log("--- Normal Block Selected! ---");
                GetComponent<NormalBlock>().PutObject();
            }
        }
    }

    public void ResetBlock()
    {
        modified = false;
        blkRenderer.color = baseColor;
    }

    public void StartBlink()
    {
        isBlink = true;
        eTime = 0;
        blinkOn = false;
    }

    public void StopBlink()
    {
        isBlink = false;
        blinkOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        Color c;
        
        if (isBlink)
        {
            eTime += Time.deltaTime;
            if (eTime > 0.5f)
            {
                eTime = 0.0f;
                if (blinkOn)
                {
                    blinkOn = false;
                    c = blkRenderer.color;
                    c.a = 1.0f;
                    blkRenderer.color = c;
                }
                else
                {
                    blinkOn = true;
                    c = blkRenderer.color;
                    c.a = 0.3f;
                    blkRenderer.color = c;
                }
            }
        }
    }

    //public void SetBlockDiff(CallBack act)
    //{
    //    blockAction = act;
    //}
}
