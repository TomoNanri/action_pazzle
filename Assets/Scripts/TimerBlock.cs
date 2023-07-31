using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBlock : MonoBehaviour
{
    public float appearTime = 5.0f;
    public float blinkStartTime = 0.5f;
    BlockSelect blkSel;   // BlockSlect スクリプトのインスタンス
    BoxCollider2D blockCollider;
    float eTime = 0.0f;
    bool isActive = false;
    bool isBlink = false;

    // Start is called before the first frame update
    void Start()
    {
        blockCollider = GetComponent<BoxCollider2D>();
        blkSel = GetComponent<BlockSelect>();
        //blkSel.SetBlockDiff(new CallBack(this.Appear));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            eTime += Time.deltaTime;

            if (eTime >= appearTime)
            {
                blockCollider.isTrigger = true;
                blkSel.StopBlink();
                isBlink = false;
                blkSel.ResetBlock();
                isActive = false;
            }
            else if (eTime >= blinkStartTime && !isBlink)
            {
                isBlink = true;
                blkSel.StartBlink();
            }
        }
    }

    public void Appear()
    {
        Debug.Log("+++ Timer Block Called Back! +++");

        isActive = true;
        blockCollider.isTrigger = false;
        eTime = 0.0f;
    }
}
