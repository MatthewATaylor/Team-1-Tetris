using UnityEngine;

public class Block : MonoBehaviour
{
    // Set type for using proper wall kick tests
    private enum Type
    {
        JLSTZ, I, O
    }

    private static readonly Vector2Int[,] wallKickTests_JLSTZ =
    {
        { new Vector2Int(0, 0), new Vector2Int(0, 0), new Vector2Int(0, 0), new Vector2Int(0, 0), new Vector2Int(0, 0) },
        { new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2), new Vector2Int(1, 2) },
        { new Vector2Int(0, 0), new Vector2Int(0, 0), new Vector2Int(0, 0), new Vector2Int(0, 0), new Vector2Int(0, 0) },
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, 2), new Vector2Int(-1, 2) }
    };

    private static readonly Vector2Int[,] wallKickTests_I =
    {
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(2, 0), new Vector2Int(-1, 0), new Vector2Int(2, 0) },
        { new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(1, -2) },
        { new Vector2Int(0, 0), new Vector2Int(2, 0), new Vector2Int(-1, 0), new Vector2Int(2, -1), new Vector2Int(-1, -1) },
        { new Vector2Int(0, 0), new Vector2Int(0, 0), new Vector2Int(0, 0), new Vector2Int(0, -2), new Vector2Int(0, 1) }
    };

    // Parent transform of four tiles
    public Transform Tiles { get; private set; }

    [SerializeField] private Transform rotationPoint;
    [SerializeField] private Type blockType;

    private int rotationPosition = 0;
    private bool canMove = true;
    private float prevTime;
    private Playboard board;
    private Score score;

    void Start()
    {
        Tiles = transform.Find(GlobalNames.tiles);

        prevTime = Time.time;
        board = GameObject.Find(GlobalNames.board).GetComponent<Playboard>();
        score = GameObject.Find(GlobalNames.score).GetComponent<Score>();
    }

    void Update()
    {
        // Update time for tile shaders (for adding wobble when drunk)
        foreach (Transform tile in Tiles)
        {
            GameObject tileObj = tile.gameObject;
            tileObj.GetComponent<Renderer>().material.SetFloat("time_s", Time.time);
            tileObj.GetComponent<Renderer>().material.SetFloat("drunkness", score.GetProgress());
        }

        if (!canMove)
        {
            return;
        }

        Vector2 displacement = new Vector2();

        // Y displacement
        float fallDelay = 1;
        if (Input.GetKey(KeyCode.DownArrow))
        {
            fallDelay = 0.1f;
        }
        if (Time.time - prevTime > fallDelay)
        {
            displacement.y -= 1;
            prevTime = Time.time;
        }

        // X displacement
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            displacement.x -= 1;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            displacement.x += 1;
        }

        // X displacement checks
        Displace(displacement.x, 0);
        foreach (Transform tile in Tiles)
        {
            if (IsPlacementConflict())
            {
                Displace(-displacement.x, 0);
                break;
            }
        }

        // Y displacement checks
        Displace(0, displacement.y);
        foreach (Transform tile in Tiles)
        {
            if (IsPlacementConflict())
            {
                Displace(0, -displacement.y);
                // TO ADD: spawn new block
                board.AddBlockToGrid(this);
                canMove = false;
                break;
            }
        }

        ControlRotation();
    }

    private void ControlRotation()
    {
        // O block doesn't rotate
        if (blockType == Type.O)
        {
            return;
        }

        int prevRotationPosition = rotationPosition;
        int rotationDirection;

        // Set rotation direction (positive for counter-clockwise, negative for clockwise)
        if (Input.GetKeyDown(KeyCode.Z))
        {
            rotationDirection = 1;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            rotationDirection = -1;
        }
        else
        {
            return;
        }

        transform.RotateAround(rotationPoint.position, Vector3.forward, 90 * rotationDirection);

        // Update rotation direction (0 to 3 from spawn configuration clockwise around)
        rotationPosition -= rotationDirection;
        if (rotationPosition < 0)
        {
            rotationPosition = 3;
        }
        else if (rotationPosition > 3)
        {
            rotationPosition = 0;
        }

        ApplyWallKick(prevRotationPosition, 90 * rotationDirection);
    }

    private void ApplyWallKick(int prevRotationPosition, float rotationAmount)
    {
        // For each of the 5 wall kick tests
        Vector2Int displacement = new Vector2Int(0, 0);
        for (int i = 0; i < 5; ++i)
        {
            // Subtract previous rotation position test with new rotation position test
            // See https://tetris.wiki/Super_Rotation_System for more info
            if (blockType == Type.JLSTZ)
            {
                displacement = wallKickTests_JLSTZ[prevRotationPosition, i] - wallKickTests_JLSTZ[rotationPosition, i];
            }
            else
            {
                displacement = wallKickTests_I[prevRotationPosition, i] - wallKickTests_I[rotationPosition, i];
            }
            Displace(displacement.x, displacement.y);

            // Wall kick successful
            if (!IsPlacementConflict())
            {
                return;
            }

            // Wall kick was unsuccessful, undo displacement
            Displace(-displacement.x, -displacement.y);
        }

        // No wall kick worked, revert back to original rotation position
        transform.RotateAround(rotationPoint.position, Vector3.forward, -rotationAmount);
        rotationPosition = prevRotationPosition;
    }

    private bool IsPlacementConflict()
    {
        // Block not outside of board's size and not overlapping another block
        foreach (Transform tile in Tiles)
        {
            if (Mathf.Abs(tile.position.x) > Playboard.w / 2.0f ||
                Mathf.Abs(tile.position.y) > Playboard.h / 2.0f ||
                board.TilePlacedAtTransform(tile))
            {
                return true;
            }
        }
        return false;
    }

    private void Displace(float x, float y)
    {
        transform.position = new Vector3(
            transform.position.x + x,
            transform.position.y + y,
            transform.position.z
        );
    }
}
