using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBlock : MonoBehaviour
{
    public GameObject[] item;
    BlockSelect blkSel;   // BlockSlect スクリプトのインスタンス
    BoxCollider2D blockCollider;

    // Start is called before the first frame update
    void Start()
    {
        blockCollider = GetComponent<BoxCollider2D>();
        blkSel = GetComponent<BlockSelect>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

    }

    public void PutObject()
    {
        //Debug.Log("+++ Normal Block Called Back! +++");
        if(GameManager.current.selectedItem != (int)Item.None) {
            Debug.Log("Put Item = " + (int)GameManager.current.selectedItem);
            //Instantiate(item[(int)GameManager.current.selectedItem], transform.position, Quaternion.identity);
        }
    }
}
