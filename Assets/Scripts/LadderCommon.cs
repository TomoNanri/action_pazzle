using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderCommon : MonoBehaviour
{
    public int turnDir = 0;

    /// <summary>
    /// 
    /// 梯子と豆の木で共通のコンポーネント
    /// Been/Ladder スクリプトの Start() から呼び出されて、
    /// turnDir （降りた後の player の進むべき方角）を確定する。
    /// 
    /// </summary>
    public void CheckTopEnd()
    {
        Transform floor;
        GameObject ladderTop = transform.Find("LadderTop").gameObject;

        Vector3 topPos = ladderTop.transform.position;  // 梯子の先端座標を取得
        topPos.x = Mathf.Round(topPos.x);
        topPos.y = Mathf.Round(topPos.y);

        // 梯子先端の右側座標を取得
        Vector3 right = topPos;
        right.x = Mathf.Round(right.x + 1);

        // 梯子先端の左側座標を取得
        Vector3 left = topPos;
        left.x = Mathf.Round(left.x - 1);

        //Debug.Log("Ladder Top X = " + topPos.x + ", Y = " + topPos.y);

        if (topPos.y == 4.0f)
        {
            floor = GameObject.Find("Floor2").transform;
        }
        else if (topPos.y == 8.0f)
        {
            floor = GameObject.Find("Floor3").transform;
        }
        else
        {
            floor = GameObject.Find("Floor4").transform;
        }

        // 到達点の左右のグラウンド有無をチェックして、グラウンドのある方へ向きを変える

        if (FindGround(floor, right))
        {
            if (FindGround(floor, left))
            {
                //Debug.Log(gameObject.name + " Both Ditected!");
                turnDir = (Random.Range(-1.0f, 1.0f) > 0) ? 1 : -1; // 左右あるならランダム
            }
            else
            {
                //Debug.Log(gameObject.name + " Right Ditected!");
                turnDir = 1;
            }
        }
        else
        {
            if (FindGround(floor, left))
            {
                //Debug.Log(gameObject.name + " Left Ditected!");
                turnDir = -1;
            }
            else
            {
                //Debug.Log(gameObject.name + " Not Ditected!");
                turnDir = (Random.Range(-1.0f, 1.0f) > 0) ? 1 : -1; // 左右とも無いならランダム
            }
        }

        //Debug.Log("Ladder TurnDir = " + turnDir);
    }

    /// <summary>
    /// 
    /// 指定したフロアの指定した方向に Ground オブジェクトがあれば真を返す
    /// 
    /// </summary>
    /// <param name="floor"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    bool FindGround(Transform floor, Vector3 pos)
    {
        foreach (Transform child in floor)
        {
            if (child.position.x == pos.x)
            {
                //Debug.Log("FLOOR Position = " + child.position.x);
                return true;
            }
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
