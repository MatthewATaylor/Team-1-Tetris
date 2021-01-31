using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject[] blocksToSpawn;

    void Start()
    {
        SpawnBlock();
    }

    void Update()
    {
        
    }

    public void SpawnBlock()
    {
        int blockIndex = Random.Range(0, blocksToSpawn.Length);
        Instantiate(blocksToSpawn[blockIndex], transform.position, Quaternion.identity);
    }
}
