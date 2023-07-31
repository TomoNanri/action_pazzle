using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanScript : MonoBehaviour
{
    // LadderCommon コンポーネント
    LadderCommon ladderCom;

    // Start is called before the first frame update
    void Start()
    {
        ladderCom = GetComponent<LadderCommon>();

        ladderCom.CheckTopEnd();
        foreach(Transform child in transform)
        {
            SpriteRenderer sprite = child.GetComponent<SpriteRenderer>();
            sprite.color += new Color(0, 0, 0, -255);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
