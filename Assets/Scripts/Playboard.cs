using UnityEngine;

public class Playboard : MonoBehaviour
{
    public static int w = 10;
    public static int h = 20;
    public static Transform[,] grid = new Transform[w, h];

    void Start()
    {
        //run block spawn code
    }

    void Update()
    {
        //gameplay
        //when block dropped, run UpdateScoreDrop()
        //if line cleared, run UpdateScoreRowClear(int num_lines)
        //run UpdateLevel(int num_lines)
    }

    public bool TilePlacedAtTransform(Transform transform)
    {
        // Check if a transform has been placed in grid at same location as transform argument
        return false;
    }
}
