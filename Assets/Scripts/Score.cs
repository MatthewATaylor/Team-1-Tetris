﻿using UnityEngine;

public class Score : MonoBehaviour
{
    public const int maxLevel = 16;

    public int score;

    [Range(0.0f, 16.0f)]
    public int level;

    void Start()
    {
        score = 0;
        level = 1;
    }

    public float GetProgress()
    {
        // Return fraction of game completed
        return (float)(level - 1) / (maxLevel - 1);
    }

    void UpdateScoreDrop(Sprite block)
    {
        score += 2 * block.GetPhysicsShapeCount();
    }

    void UpdateScoreRowClear(int num_lines)
    {
        if (num_lines == 1)
        {
            score += 40 * (num_lines + 1);
        }
        else if (num_lines == 2)
        {
            score += 100 * (num_lines + 1);
        }
        else if (num_lines == 3)
        {
            score += 300 * (num_lines + 1);
        }
        else if (num_lines == 4)
        {
            score += 1200 * (num_lines + 1);
        }
        else
        {
            score += 2000 * (num_lines + 1);
        }
    }

    void UpdateLevel(int num_lines)
    {
        if (num_lines % (4 * level) == 0)
        {
            level += 1;
        }
    }
}
