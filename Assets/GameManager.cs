using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void OnDirection(int direction)
    {
        switch((Direction)direction)
        {
            case Direction.Up:
                Runner.Instance.StartMove(Vector2.up);
                break;
            case Direction.Right:
                Runner.Instance.StartMove(Vector2.right);
                break;
            case Direction.Down:
                Runner.Instance.StartMove(Vector2.down);
                break;
            case Direction.Left:
                Runner.Instance.StartMove(Vector2.left);
                break;
        }
    }
}

public enum Direction { Up = 0, Right, Down, Left }
