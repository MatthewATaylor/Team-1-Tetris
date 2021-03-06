﻿using System.Collections.Generic;
using UnityEngine;

public class Playboard : MonoBehaviour
{
    private class SavedTile
    {
        public Transform tile;
        public ParticleSystem particleSystem;
        public Block.Type blockType;

        public SavedTile(Transform tile, ParticleSystem particleSystem, Block.Type blockType)
        {
            this.tile = tile;
            this.particleSystem = particleSystem;
            this.blockType = blockType;
        }
    }

    public const int w = 10;
    public const int h = 18;

    [SerializeField] private AudioSource glassBreakSource;

    private SavedTile[,] grid = new SavedTile[w, h];

    private Score score;
    private int totalRowsCleared = 0;
    private List<ParticleSystem> activeParticleSystems = new List<ParticleSystem>();

    void Start()
    {
        score = GameObject.Find(GlobalNames.score).GetComponent<Score>();
    }

    void Update()
    {
        // Update shader uniforms for wobble effect
        Shader.SetGlobalFloat("time_s", Time.time);
        Shader.SetGlobalFloat("drunkness", 0.125f * score.Level);

        // Remove dead particle systems
        int i = 0;
        while (i < activeParticleSystems.Count)
        {
            if (activeParticleSystems[i].IsAlive())
            {
                ++i;
            }
            else
            {
                Destroy(activeParticleSystems[i].gameObject);
                activeParticleSystems.RemoveAt(i);
            }
        }
    }

    public Vector2 GetRelativePosition(Vector3 worldPosition)
    {
        // Get position of transform relative to Playboard
        return worldPosition - transform.position;
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
            grid[indices.x, indices.y] = new SavedTile(tile, block.explosionParticleSystem, block.blockType);
        }
        ClearFullRows();
    }

    private void ClearFullRows()
    {
        int numRowsCleared = 0;
        bool prevRowWasCleared = false;

        for (int i = h - 1; i >= 0; --i)
        {
            bool rowIsFull = true;
            for (int j = 0; j < w; ++j)
            {
                if (grid[j, i] == null || (grid[j, i].blockType == Block.Type.Xanax && !prevRowWasCleared))
                {
                    rowIsFull = false;
                    prevRowWasCleared = false;
                    break;
                }
            }
            if (rowIsFull)
            {
                for (int j = 0; j < w; ++j)
                {
                    if (grid[j, i] != null)
                    {
                        // Add explosion particle system
                        ParticleSystem explosion = Instantiate(grid[j, i].particleSystem, grid[j, i].tile.position, Quaternion.identity);
                        explosion.GetComponent<Renderer>().sortingOrder = 2;
                        activeParticleSystems.Add(explosion);

                        // Clear row
                        Destroy(grid[j, i].tile.gameObject);
                    }

                    // Shift above tiles down
                    for (int k = i + 1; k < h; ++k)
                    {
                        grid[j, k - 1] = grid[j, k];

                        if (grid[j, k] == null)
                        {
                            continue;
                        }

                        Vector2 tilePosition = grid[j, k].tile.position;
                        tilePosition.y -= 1;
                        grid[j, k].tile.position = tilePosition;
                    }

                    // Make top row null
                    for (int k = 0; k < w; ++k)
                    {
                        if (grid[k, h - 1] == null)
                        {
                            continue;
                        }

                        Destroy(grid[k, h - 1].tile.gameObject);
                        grid[k, h - 1] = null;
                    }
                }

                ++numRowsCleared;
                prevRowWasCleared = true;
            }
        }

        if (numRowsCleared > 0)
        {
            totalRowsCleared += numRowsCleared;
            score.UpdateScoreRowClear(numRowsCleared);
            score.UpdateLevel(totalRowsCleared);
            glassBreakSource.Play();
        }
    }

    private Vector2Int GetIndicesOfTransform(Transform transform)
    {
        Vector2 relativePosition = GetRelativePosition(transform.position);
        int xIndex = Mathf.RoundToInt(relativePosition.x - 0.5f) + w / 2;
        int yIndex = Mathf.RoundToInt(relativePosition.y - 0.5f) + h / 2;
        return new Vector2Int(xIndex, yIndex);
    }
}
