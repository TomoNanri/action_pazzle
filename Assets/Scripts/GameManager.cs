using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
enum Item : int {Rock, RightJump, LeftJump, None }

public class GameManager : MonoBehaviour
{
    public static GameManager current;
    public bool isGameOver = false;
    public bool giveUp = false;
    public bool isPlayerDied = false;
    public bool hasGotGoalKey = false;
    public bool hasGotGoal = false;
    public int remainingTime = 60;
    public int Life = 5;
    public int rockCount = 5;
    public int rightJumpCount = 0;
    public int leftJumpCount = 0;
    public int timerBlockCount = 5;
    public int selectedItem = (int)Item.Rock;
    public Text TimerText;
    public Text ScoreText;
    public Text LifeText;
    public GameObject GameClearText;
    public GameObject GameOverText;
    public Text SetRockButtonLabel;
    public Text SetRightJumpButtonLabel;
    public Text SetLeftJumpButtonLabel;
    public Text TimerBlockCountLabel;

    private float elapthedTime = 0f;
    int score = 0;

    void Awake()
    {
        if (current == null)
        {
            current = this;
        }
        else if (current != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        TimerText.text = "TIME : " + remainingTime;
        ScoreText.text = "SCORE : " + score;
        LifeText.text = "LIFE : " + Life;

        SetRockButtonLabel.text = " ×" + rockCount;
        SetRightJumpButtonLabel.text = " ×" + rightJumpCount;
        SetLeftJumpButtonLabel.text = " ×" + leftJumpCount;
        TimerBlockCountLabel.text = " ×" + timerBlockCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            if (hasGotGoal)
            {
                GameClear();
            }
            else
            {
                TimerText.text = "TIME : " + remainingTime;
                ScoreText.text = "SCORE : " + score;
                LifeText.text = "LIFE : " + Life;
                SetRockButtonLabel.text = " ×" + rockCount;
                SetRightJumpButtonLabel.text = " ×" + rightJumpCount;
                SetLeftJumpButtonLabel.text = " ×" + leftJumpCount;
                TimerBlockCountLabel.text = " ×" + timerBlockCount;

            }
        }
    }

    public void AddPoint(int addValue)
    {
        score += addValue;
    }

    public void AddItem(string item)
    {
        switch (item)
        {
            case "Rock":
                if (rockCount < 5)
                {
                    rockCount++;
                }
                break;

            case "TimerBlock":
                if (timerBlockCount < 5)
                {
                    timerBlockCount++;
                }
                break;

            case "RightJump":
                if (rightJumpCount < 5)
                {
                    rightJumpCount++;
                }
                break;

            case "LeftJump":
                if (leftJumpCount < 5)
                {
                    leftJumpCount++;
                }
                break;
            default:
                Debug.Log("+++ Logical error +++");
                break;
        }
    }

    public bool UseItem(string item)
    {
        switch (item)
        {
            case "Rock":
                if (rockCount > 0)
                {
                    rockCount--;
                    return true;
                }
                break;

            case "TimerBlock":
                if (timerBlockCount > 0)
                {
                    timerBlockCount--;
                    return true;
                }
                break;

            case "RightJump":
                if (rightJumpCount > 0)
                {
                    rightJumpCount--;
                    return true;
                }
                break;

            case "LeftJump":
                if (leftJumpCount > 0)
                {
                    leftJumpCount--;
                    return true;
                }
                break;

            default:
                Debug.Log("+++ Logical error +++");
                break;
        }
        return false;
    }

    void GameClear()
    {
        // GAME CLEAR の表示
        GameClearText.SetActive(true);

        // ステージの後始末

    }

    void FixedUpdate()
    {
        elapthedTime += Time.deltaTime;
        if(elapthedTime >= 1.0f)
        {
            elapthedTime = 0f;
            remainingTime--;
        }
    }

    public void GiveUpReq()
    {
        if (!hasGotGoal && !isPlayerDied)
        {
            giveUp =true;
            Debug.Log("GiveUp Request! Life = " + Life);
        }
    }

    public bool PlayerDied()
    {
        giveUp = false;
        isPlayerDied = true;
        Life--;

        if (Life > 0)
        {
            return true;            // プレイヤーの位置を戻してリスタートさせる        
        }
        else
        {
            // Game Over 処理
            isGameOver = true;
            LifeText.text = "LIFE : " + Life;

            // GAME OVER を表示する
            GameOverText.SetActive(true);

            // Player以外を停止する？

            return false;           // プレイヤーを停止させてゲーム終了
        }
    }
}
