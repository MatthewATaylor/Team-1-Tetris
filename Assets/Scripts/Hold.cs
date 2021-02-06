using UnityEngine;

public class Hold : MonoBehaviour
{
    [SerializeField] private Spawn spawn;

    private Block heldBlockPrefab;
    private GameObject blockPreview;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (heldBlockPrefab == null)
            {
                Destroy(spawn.ActiveBlock);
                heldBlockPrefab = spawn.ActiveBlockPrefab.GetComponent<Block>();
                spawn.SpawnBlock();
            }
            else
            {
                Destroy(spawn.ActiveBlock);
                Block heldBlockPrefabTemp = spawn.ActiveBlockPrefab.GetComponent<Block>();
                spawn.SpawnBlock(heldBlockPrefab);
                heldBlockPrefab = heldBlockPrefabTemp;
            }


            RefreshPreview();
        }
    }

    private void RefreshPreview()
    {
        if (blockPreview != null)
        {
            Destroy(blockPreview);
        }

        blockPreview = Instantiate(heldBlockPrefab.preview, transform.position, Quaternion.identity);
    }
}
