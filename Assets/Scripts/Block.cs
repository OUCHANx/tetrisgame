using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private bool canRotate = true;

    void Move(Vector3 moveDirection)
    {
        transform.position += moveDirection;
    }

    public void MoveLeft()
    {
        Move(Vector3.left);
    }
    public void MoveRight()
    {
        Move(Vector3.right);
    }
    public void MoveUp()
    {
        Move(Vector3.up);
    }
    public void MoveDown()
    {
        Move(Vector3.down);
    }

    //回転用
    public void RotateRight()
    {
        if (canRotate)
        {
            transform.Rotate(0,0,-90);
        }
    }
    public void RotateLeft()
    {
        if (canRotate)
        {
            transform.Rotate(0,0,90);
        }
    }
}
