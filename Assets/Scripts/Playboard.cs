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
		foreach (Transform child in transform){
			if (Playboard.grid[(int)child.position.x, (int)child.position.y] != null){
				return false;
			}
		}
        	return true;
	}
}
