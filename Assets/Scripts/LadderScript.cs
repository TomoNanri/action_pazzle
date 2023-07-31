using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    // LadderCommon コンポーネント
    LadderCommon ladderCom;

    // Start is called before the first frame update
    void Start()
    {
        ladderCom = GetComponent<LadderCommon>();

        ladderCom.CheckTopEnd();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
