#define DEBUG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Player: MonoBehaviour
{
    public float playerSpeed = 2.0f;    // Working Speed = 3m/s
    public int playerDirection = 1;     // 1:right, -1:left
    public float climbSpeed = 2.0f;
    public float jumpSpeed = 8.0f;
    private Rigidbody2D playerBody;
    private Vector3 initialPos;
    private int initialDir;
    private bool climb = false;
    private float eTime = 0;
    Animator anim;

    [SerializeField]
    float fallCheckInterval = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        initialDir = playerDirection;
        playerBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        Vector2 v = new Vector2(playerDirection, 0);
        Move(v.normalized, playerSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.current.hasGotGoal)
        {
            anim.SetTrigger("GetGoal");
            Vector2 v = new Vector2(0, 0);
            Move(v, playerSpeed);
        }
    }

    void FixedUpdate()
    {
        if (!GameManager.current.isGameOver)
        {
            eTime += Time.deltaTime;
            if (eTime >= fallCheckInterval)
            {
                eTime = 0f;
                if (playerBody.velocity.y < 0)   // 落下開始
                {
                    Vector2 v = new Vector2(0, -1);
                    Move(v.normalized, playerSpeed);
                }
            }
        }
    }

    // 障害物にぶつかったときの処理
    void OnCollisionEnter2D(Collision2D c)
    {
        if (!GameManager.current.hasGotGoal)
        {
            // 衝突した相手のレイヤー名を取得する
            string layerName = LayerMask.LayerToName(c.gameObject.layer);

            if (layerName == "Ground"|| layerName == "DummyBlock")
            {
                if (!climb)
                {
                    Vector2 v = new Vector2(playerDirection, 0.0f);
                    Move(v.normalized, playerSpeed);
                }
            }
            else if (layerName == "Obstacle")
            {
                Reverse();
                playerDirection *= -1;
                Vector2 v = new Vector2(playerDirection, 0.1f);
                Move(v.normalized, playerSpeed);
            }

            else if (layerName == "AstroWalk")
            {
                // プレイヤーダメージ処理
                LostLife();
            }
        }
    }

    public void LostLife()
    {
        anim.SetTrigger("Damaged");
        if (GameObject.Find("GameManager").GetComponent<GameManager>().PlayerDied())
        {
            transform.position = initialPos;
            if (playerDirection != initialDir)
            {
                Reverse();
                playerDirection = initialDir;
            }
            Vector2 v = new Vector2(playerDirection, 0);
            Move(v.normalized, playerSpeed);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Restart()
    {
        Vector2 v = new Vector2(playerDirection, 0);
        Move(v.normalized, playerSpeed);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (!GameManager.current.isGameOver)
        {
            // 接触中の相手のレイヤー名を取得する
            string layerName = LayerMask.LayerToName(c.gameObject.layer);

            if (layerName == "LadderTop")
            {
                if (climb)
                {
                    climb = false;
                    Vector3 pos = transform.position;
                    pos.y += 1.2f;
                    transform.position = pos;

                    LadderCommon s = c.gameObject.GetComponentInParent<LadderCommon>();
                    //Debug.Log("New Direction = " + playerDirection);

                    if (playerDirection != s.turnDir)
                    {
                        Reverse();
                        playerDirection *= -1;
                        Vector2 v = new Vector2(playerDirection, 0);
                        Move(v, playerSpeed);
                    }
                    else
                    {
                        Vector2 v = new Vector2(playerDirection, 0);
                        Move(v, playerSpeed);
                    }
                }
            }

            else if (layerName == "LadderBase")
            {
                climb = true;
                Vector3 pos = transform.position;
                pos.x = Mathf.Floor(pos.x) + 0.5f;  // セルの中央にリポジ
                pos.y += 0.5f;
                transform.position = pos;
                Vector2 v = new Vector2(0, 1);      // PlyerのＸ方向の動きを停止して上に運ぶ
                Move(v.normalized, climbSpeed);
            }

            else if (layerName == "JumpRight")
            {
                if (playerDirection != 1)
                {
                    Reverse();
                }
                playerDirection = 1;
                Jump(playerDirection);
            }

            else if (layerName == "JumpLeft")
            {
                if (playerDirection != -1)
                {
                    Reverse();
                }
                playerDirection = -1;
                Jump(playerDirection);
            }
        }

    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (!GameManager.current.isGameOver)
        {
            // 接触中の相手のレイヤー名を取得する
            string layerName = LayerMask.LayerToName(c.gameObject.layer);

            if (layerName == "LadderMid")
            {
                if (climb)
                {
                    Vector3 pos = transform.position;
                    pos.y += 0.15f;
                    transform.position = pos;
                }
            }
        }
    }

    void Jump(int d)
    {
        //Vector3 pos = transform.position;
        //pos.y += 1.0f;
        //transform.position = pos;
        Vector2 v = new Vector2((float)d, 1.2f);
        Move(v.normalized, jumpSpeed);
    }

    void Reverse()
    {
        Vector3 reverseVector = transform.localScale;
        reverseVector.x *= -1.0f;
        transform.localScale = reverseVector;
    }

    void Move(Vector2 direction, float speed)
    {
        playerBody.velocity = direction * speed;
        //Debug.Log("player velocity x = " + playerBody.velocity.x);
    }
}
