using UnityEngine;

public class Spawn : MonoBehaviour
{
    public const int queueSize = 3;

    private Block[] blockQueue = new Block[queueSize];

    [SerializeField] private BlockPreview preview;
    [SerializeField] private Block[] blocks;

    void Start()
    {
        for (int i = 0; i < queueSize; ++i)
        {
            blockQueue[i] = GetRandomBlock();
        }

        SpawnBlock();
    }

    void Update()
    {
        
    }

    public void SpawnBlock()
    {
        // Instantiate first tetromino in queue
        GameObject newBlock = Instantiate(blockQueue[0].gameObject, transform.position, Quaternion.identity);
        newBlock.transform.SetParent(transform.parent);  // Set parent to Playboard

        // Shift queue forwards
        for (int i = 0; i < queueSize - 1; ++i)
        {
            blockQueue[i] = blockQueue[i + 1];
        }

        // Add new tetromino to end of queue
        blockQueue[queueSize - 1] = GetRandomBlock();

        preview.Refresh(blockQueue);
    }

    private Block GetRandomBlock()
    {
        int index = Random.Range(0, blocks.Length);
        return blocks[index];
    }
}
