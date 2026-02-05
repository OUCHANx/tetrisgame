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
        nextdropTimer = Time.time + dropInterval;
        //スポナークラスからブロック生成関数を読んで変数に格納する
        if(!activeBlock)
        {
            activeBlock = spawner.SpawnBlock();
        }
    }

    private void Update()
    {
        PlayerInput();
        AutoDrop();
    }

    void PlayerInput()
{
    var kb = Keyboard.current;
    if (kb == null) return;

    bool right = (kb.dKey.isPressed && Time.time > nextKeyLeftRightTimer) || kb.dKey.wasPressedThisFrame;
    if (right)
    {
        activeBlock.MoveRight();
        nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;
        if (!board.CheckPosition(activeBlock)) activeBlock.MoveLeft();
    }

    bool left  = (kb.aKey.isPressed && Time.time > nextKeyLeftRightTimer) || kb.aKey.wasPressedThisFrame;
    if (left)
    {
        activeBlock.MoveLeft();
        nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;
        if (!board.CheckPosition(activeBlock)) activeBlock.MoveRight();
    }

    bool rotate = (kb.eKey.isPressed && Time.time > nextKeyRotateTimer) || kb.eKey.wasPressedThisFrame;
    if (rotate)
    {
        activeBlock.RotateRight();
        nextKeyRotateTimer = Time.time + nextKeyRotateInterval;
        if (!board.CheckPosition(activeBlock)) activeBlock.RotateLeft();
    }

    bool down = (kb.sKey.isPressed && Time.time > nextKeyDownTimer) || kb.sKey.wasPressedThisFrame;
    if (down)
    {
        activeBlock.MoveDown();
        nextKeyDownTimer = Time.time + nextKeyDownInterval;
        nextdropTimer = Time.time + dropInterval;
        if (!board.CheckPosition(activeBlock)) BottomBoard();
    }
}

    void AutoDrop()
    {
        if (Time.time <= nextdropTimer) return;

        activeBlock.MoveDown();
        nextdropTimer = Time.time + dropInterval;

        if (!board.CheckPosition(activeBlock))
        {
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
            nextdropTimer = Time.time + dropInterval;


            board.ClearAllRows();
        }
    }

