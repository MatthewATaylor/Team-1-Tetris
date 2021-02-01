using UnityEngine;

public class Playboard : MonoBehaviour
{
 	public static int w = 10;
	public static int h = 20;
	public static Transform[,] grid = new Transform[w, h];

	private Score score;
    private int totalRowsCleared = 0;

	void Start()
 	{
        score = GameObject.Find(GlobalNames.score).GetComponent<Score>();
    }

	void Update()
    {
        //when block dropped, run UpdateScoreDrop()
	}

	public bool TilePlacedAtTransform(Transform transform)
  	{
        Vector2Int indices = GetIndicesOfTransform(transform);
        try
        {
            return grid[indices.x, indices.y] != null;
        }
        catch (System.IndexOutOfRangeException)
        {
            return false;
        }
    }

    public void AddBlockToGrid(Block block)
    {
        foreach (Transform tile in block.Tiles)
        {
            Vector2Int indices = GetIndicesOfTransform(tile);
            grid[indices.x, indices.y] = tile;
        }
        ClearFullRows();
    }

    private void ClearFullRows()
    {
        int numRowsCleared = 0;
        for (int i = h - 1; i >= 0; --i)
        {
            bool rowIsFull = true;
            for (int j = 0; j < w; ++j)
            {
                if (grid[j, i] == null)
                {
                    rowIsFull = false;
                    break;
                }
            }
            if (rowIsFull)
            {
                for (int j = 0; j < w; ++j)
                {
                    // Clear row
                    Destroy(grid[j, i].gameObject);

                    // Shift above tiles down
                    for (int k = i + 1; k < h; ++k)
                    {
                        grid[j, k - 1] = grid[j, k];

                        if (grid[j, k] == null)
                        {
                            continue;
                        }

                        Vector2 tilePosition = grid[j, k].position;
                        tilePosition.y -= 1;
                        grid[j, k].position = tilePosition;
                    }

                    // Make top row null
                    for (int k = 0; k < w; ++k)
                    {
                        if (grid[k, h - 1] == null)
                        {
                            continue;
                        }

                        Destroy(grid[k, h - 1].gameObject);
                        grid[k, h - 1] = null;
                    }
                }

                ++numRowsCleared;
            }
        }

        if (numRowsCleared > 0)
        {
            totalRowsCleared += numRowsCleared;
        }
    }

    private Vector2Int GetIndicesOfTransform(Transform transform)
    {
        int xIndex = Mathf.RoundToInt(transform.position.x - 0.5f) + w / 2;
        int yIndex = Mathf.RoundToInt(transform.position.y - 0.5f) + h / 2;
        return new Vector2Int(xIndex, yIndex);
    }
}
