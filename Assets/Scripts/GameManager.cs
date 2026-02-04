using UnityEngine;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{
    //やること
    //変数の作成
    //スポナー
    //生成されたブロック格納

    Spawner spawner;
    Block activeBlock;
    [SerializeField]
    private float dropInterval = 0.25f;
    float nextdropTimer;

    //ボードのスクリプト格納
    Board board;


    float nextKeyDownTimer, nextKeyLeftRightTimer, nextKeyRotateTimer;

    [SerializeField]
    private float nextKeyDownInterval, nextKeyLeftRightInterval, nextKeyRotateInterval;

    //スポナーオブジェクトをスポナー変数に格納するコードの記述
    private void Start()
    {
            //スポナーオブジェクトをスポナー変数に格納するコードの記述

        spawner = GameObject.FindAnyObjectByType<Spawner>();
        //ボードを変数に格納する
        board = GameObject.FindAnyObjectByType<Board>();

        spawner.transform.position = Rounding.Round(spawner.transform.position);
        //タイマーの初期設定
        nextKeyDownTimer = Time.time + nextKeyDownInterval;
        nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;
        nextKeyRotateTimer = Time.time + nextKeyRotateInterval;
        //スポナークラスからブロック生成関数を読んで変数に格納する
        if(!activeBlock)
        {
            activeBlock = spawner.SpawnBlock();
        }
    }

    private void Update()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        if (Input.GetKey(KeyCode.D) && (Time.time > nextKeyLeftRightTimer)
         || Input.GetKeyDown(KeyCode.D))
        {
            activeBlock.MoveRight();
            nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;

            if (!board.CheckPosition(activeBlock))
            {
                activeBlock.MoveLeft();
            }
        }
        else if (Input.GetKey(KeyCode.A) && (Time.time > nextKeyLeftRightTimer)
        || Input.GetKeyDown(KeyCode.A))
        {
            activeBlock.MoveLeft();
            nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;

            if (!board.CheckPosition(activeBlock))
            {
                activeBlock.MoveRight();
            }
        }
        else if (Input.GetKey(KeyCode.E) && (Time.time > nextKeyRotateTimer)
         || Input.GetKeyDown(KeyCode.E))
        {
            activeBlock.RotateRight();
            nextKeyRotateTimer = Time.time + nextKeyRotateInterval;

            if (!board.CheckPosition(activeBlock))
            {
                activeBlock.RotateLeft();
            }
        }
        else if (Input.GetKey(KeyCode.S) && (Time.time > nextKeyDownTimer)
         || Input.GetKeyDown(KeyCode.S))
        {
            activeBlock.MoveDown();
            nextKeyDownTimer = Time.time + nextKeyDownInterval;
            nextdropTimer = Time.time + dropInterval;

            if (!board.CheckPosition(activeBlock))
            {
                //そこについた時の処理
                BottomBoard();
            }
        }

        void BottomBoard()
        {
            activeBlock.MoveUp();
            board.SaveBlockInGrid(activeBlock);

            activeBlock = spawner.SpawnBlock();


            nextKeyDownTimer = Time.time;
            nextKeyLeftRightTimer = Time.time;
            nextKeyRotateTimer = Time.time;
        }
    }

}
