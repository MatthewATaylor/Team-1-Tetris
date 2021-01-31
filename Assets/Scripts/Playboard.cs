using UnityEngine;

public class Playboard : MonoBehaviour
{
 	public static int w = 10;
	public static int h = 20;
	public static Transform[,] grid = new Transform[w, h];

	private Score score;

	void Start()
 	{
        score = GameObject.Find(GlobalNames.score).GetComponent<Score>();
    }

	void Update()
    {
		//when block dropped, update grid
        //when block dropped, run UpdateScoreDrop()
        //if line cleared, run UpdateScoreRowClear(int num_lines)
 		//run UpdateLevel(int num_lines)
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
    }

    private Vector2Int GetIndicesOfTransform(Transform transform)
    {
        int xIndex = Mathf.RoundToInt(transform.position.x - 0.5f) + w / 2;
        int yIndex = Mathf.RoundToInt(transform.position.y - 0.5f) + h / 2;
        return new Vector2Int(xIndex, yIndex);
    }
}
