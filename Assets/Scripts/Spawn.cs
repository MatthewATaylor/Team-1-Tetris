using UnityEngine;

public class Spawn : MonoBehaviour
{
    public const int queueSize = 3;

    public GameObject ActiveBlock { get; private set; }
    public GameObject ActiveBlockPrefab { get; private set; }

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
        SpawnBlock(blockQueue[0]);

        // Shift queue forwards
        for (int i = 0; i < queueSize - 1; ++i)
        {
            blockQueue[i] = blockQueue[i + 1];
        }

        // Add new tetromino to end of queue
        blockQueue[queueSize - 1] = GetRandomBlock();

        preview.Refresh(blockQueue);
    }

    public void SpawnBlock(Block blockPrefab)
    {
        ActiveBlock = Instantiate(blockPrefab.gameObject, transform.position, Quaternion.identity);
        ActiveBlock.transform.SetParent(transform.parent);  // Set parent to Playboard
        ActiveBlockPrefab = blockPrefab.gameObject;
    }

    private Block GetRandomBlock()
    {
        int index = Random.Range(0, blocks.Length);
        return blocks[index];
    }
}
