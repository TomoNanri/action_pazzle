using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroWalk : MonoBehaviour
{
    public float astroWalkSpeed = 0.5f;
    private Rigidbody2D astroWalkBody;
    private FootScript leftFoot;
    private FootScript rightFoot;
    public bool isFalling = false;

    [SerializeField]
    private int moveDir = 1;    // 1:right, -1:left

    // Start is called before the first frame update
    void Start()
    {
        astroWalkBody = GetComponent<Rigidbody2D>();
        leftFoot = GameObject.Find("LeftFoot").GetComponent<FootScript>();
        rightFoot = GameObject.Find("RightFoot").GetComponent<FootScript>();

        Debug.Log("START ASTROWALK. Velocity=" + astroWalkBody.velocity);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.current.isGameOver)
        {
            if (isFalling){
                if (leftFoot.isOnGround && rightFoot.isOnGround)
                {
                    isFalling = false;
                }
                else
                {
                    // NOP
                    //astroWalkBody.velocity = Vector2.down;
                }
            }
            else
            {
                if (!leftFoot.isOnGround && !rightFoot.isOnGround)
                {
                    isFalling = true;
                    //astroWalkBody.velocity = Vector2.down;
                }
                else
                {
                    if (!leftFoot.isOnGround)
                    {
                        Reverse();
                    }
                    Move();
                }
            }
        }
    }

    void Reverse()
    {
        moveDir *= -1;
        Vector3 reverseVector = transform.localScale;
        reverseVector.x *= -1.0f;
        transform.localScale = reverseVector;
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (!GameManager.current.isGameOver)
        {
            string layerName = LayerMask.LayerToName(c.gameObject.layer);
            if(layerName == "Obstacle" || layerName == "LadderBase")
            {
                Reverse();
                Move();
            }
        }
    }

    void Move()
    {
        if (moveDir > 0)
        {
            transform.Translate(Vector2.right * astroWalkSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * astroWalkSpeed * Time.deltaTime);
        }
        //Debug.Log("ASTROWALK.Move Dir = "+moveDir+", Velocity = "+astroWalkBody.velocity);
    }
}
