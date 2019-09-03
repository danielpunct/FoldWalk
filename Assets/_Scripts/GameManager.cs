using UnityEngine;

public class GameManager : MonoBehaviour
{

    /// <summary>
    /// Translate the Swipe gesture from the mouse to a direction for the player
    /// </summary>
    /// <param name="direction"></param>
    public void OnDirection(int direction)
    {
        var gridDirection = Vector3.forward;
        switch ((Direction)direction)
        {
            case Direction.Up:
                gridDirection = Vector3.back;
                break;
            case Direction.Right:
                gridDirection = Vector3.left;
                break;
            case Direction.Down:
                gridDirection = Vector3.forward;
                break;
            case Direction.Left:
                gridDirection = Vector3.right;
                break;
        }

        // Try to move the player
        Runner.Instance.SetDirection(gridDirection);
    }
}

public enum Direction { Up = 0, Right, Down, Left }
