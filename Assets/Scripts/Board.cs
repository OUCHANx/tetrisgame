using UnityEngine;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{

    //二次元配列の作成
    private Transform[,] grid;
    //やること
    //変数の作成
    //ボード基盤用の四角枠格納用
    //ボードの高さ
    //ボードのはば
    //ボードの高さ調整用数値
    
    [SerializeField]
    private Transform emptySprite;

    [SerializeField]
    private int height = 30, width = 10, header = 8;

    private void Awake()
    {
        grid = new Transform[width, height];
    }

    void Start()
    {
        CreateBoard();
    }
    //関数の作成
    //ボードを生成する関数の作成
    void CreateBoard()
    {
        if (emptySprite)
        {
            for (int y = 0; y < height-header; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Transform clone = Instantiate(emptySprite, 
                    new Vector3(x, y, 0),Quaternion.identity);
                    clone.transform.parent =  transform;
                }
            }
        }
    }

    //ブロックが枠内にあるのかを判定する関数を呼ぶ関数
    public bool CheckPosition(Block block)
    {
        foreach(Transform item in block.transform)
        {
            Vector2 pos = Rounding.Round(item.position);

            if (!BoardOutCheck((int)pos.x, (int)pos.y))
            {
                return false;
            }

            if (BlockCheck((int)pos.x, (int)pos.y, block))
            {
                return false;
            }
        }
        return true;
    }
    //枠内にあるのか判定する関数

    bool BoardOutCheck(int x, int y)
    {
        //x軸が０以上、width未満、y軸は０以上
        return (x >= 0 && x < width && y >= 0);
    }

    bool BlockCheck(int x,int y, Block block)
    {
        return(grid[x,y] != null && grid[x,y].parent != block.transform);
    }

    public void SaveBlockInGrid(Block block)
    {
        foreach(Transform item in block.transform)
        {
            Vector2 pos = Rounding.Round(item.position);
            grid[(int)pos.x, (int)pos.y] = item;
        }
    }
}
