using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Playboard : MonoBehaviour
{
    public Vector2Int Size { get; private set; }

    private bool[,] positionedTiles;

    void Start()
    {
        Vector2 sizeFloat = GetComponent<Renderer>().bounds.size;
        Size = new Vector2Int((int)sizeFloat.x, (int)sizeFloat.y);
        positionedTiles = new bool[Size.x, Size.y];
    }

    void Update()
    {

    }

    public void PositionBlock(Block block)
    {
        foreach (Transform tile in block.Tiles)
        {
            Vector2Int tileIndices = GetIndicesFromPos(tile.position);
            positionedTiles[tileIndices.x, tileIndices.y] = true;
        }
    }

    public bool TilePlacedAtTransform(Transform transform)
    {
        Vector2Int transformIndices = GetIndicesFromPos(transform.position);
        try
        {
            return positionedTiles[transformIndices.x, transformIndices.y];
        }
        catch (System.IndexOutOfRangeException)
        {
            return false;
        }
    }

    public Vector2Int GetIndicesFromPos(Vector2 position)
    {
        return new Vector2Int(
            Mathf.RoundToInt(position.x - 0.5f) + Size.x / 2,
            Mathf.RoundToInt(position.y - 0.5f) + Size.y / 2
        );
    }

    public void Display()
    {
        string output = "";
        for (int i = Size.y - 1; i >= 0; --i)
        {
            for (int j = 0; j < Size.x; ++j)
            {
                output += positionedTiles[j, i] ? "1 " : "0 ";
            }
            output += "\n";
        }
        print(output);
    }
}
