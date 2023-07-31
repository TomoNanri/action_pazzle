using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootScript : MonoBehaviour
{
    public bool isOnGround = false;
    private GameObject lastEnterGround;

    // Start is called before the first frame update
    void Start()
    {
        lastEnterGround = gameObject;   // NULLにしないためにダミーで自分を入れる
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        string layerName = LayerMask.LayerToName(c.gameObject.layer);

        if (layerName == "Ground")
        {
            lastEnterGround = c.gameObject; // 最後に入ったマス（1Enter,2Enter,1Exit)対策
            isOnGround = true;
        }
        else if(layerName == "TimerItem")
        {
            if (!c.isTrigger)
            {
                lastEnterGround = c.gameObject;
                isOnGround = true;
            }
        }
        //Debug.Log(transform.name + " Triggered Enter Layer = " + layerName);
        //Debug.Log("isOnGround = " + isOnGround);
    }

    void OnTriggerExit2D(Collider2D c)
    {
        string layerName = LayerMask.LayerToName(c.gameObject.layer);

        if (layerName == "Ground")
        {
            if(lastEnterGround == c.gameObject) // 前のマスからのExitをキャンセル
            {
                isOnGround = false;
            }
        }
        //Debug.Log(transform.name + " Triggered Exit Layer = " + layerName);
        //Debug.Log("isOnGround = " + isOnGround);
    }
}
