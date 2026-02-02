using UnityEngine;

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

    //スポナーオブジェクトをスポナー変数に格納するコードの記述
    private void Start()
    {
            //スポナーオブジェクトをスポナー変数に格納するコードの記述

        spawner = GameObject.FindAnyObjectByType<Spawner>();
        //ボードを変数に格納する
        board = GameObject.FindAnyObjectByType<Board>();

        spawner.transform.position = Rounding.Round(spawner.transform.position);

        //スポナークラスからブロック生成関数を読んで変数に格納する
        if(!activeBlock)
        {
            activeBlock = spawner.SpawnBlock();
        }
    }

    private void Update()
    {
        if (Time.time > nextdropTimer)
        {
            nextdropTimer = Time.time + dropInterval;
            if (activeBlock)
            {
                activeBlock.MoveDown();

                //updateでboardクラスの関数を呼び出してボードから出ていないか確認
                if (!board.CheckPosition(activeBlock))
                {
                    activeBlock.MoveUp();
                    board.SaveBlockInGrid(activeBlock);
                    //新しいブロックを生成して変数に格納
                    activeBlock = spawner.SpawnBlock();
                }
            }
        }
    }

}
