using UnityEngine;

public class BlockPreview : MonoBehaviour
{
    [SerializeField] private Transform[] queuePositions = new Transform[Spawn.queueSize];

    private GameObject[] blockPreviews = new GameObject[Spawn.queueSize];

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void Refresh(Block[] blockQueue)
    {
        ClearPreviewQueue();
        FillQueuePositions(blockQueue);
    }

    private void ClearPreviewQueue()
    {
        for (int i = 0; i < Spawn.queueSize; ++i)
        {
            if (blockPreviews[i] != null)
            {
                Destroy(blockPreviews[i]);
            }
        }
    }

    private void FillQueuePositions(Block[] blockQueue)
    {
        for (int i = 0; i < Spawn.queueSize; ++i)
        {
            GameObject blockPreview = blockQueue[i].preview;
            GameObject blockPreviewInstance = Instantiate(blockPreview, queuePositions[i].position, Quaternion.identity);
            blockPreviewInstance.transform.SetParent(queuePositions[i]);  // Set parent to queue position transform
            blockPreviews[i] = blockPreviewInstance;
        }
    }
}
