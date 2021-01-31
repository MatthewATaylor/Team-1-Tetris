using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject blockToSpawn;

    void Start()
    {
        SpawnBlock();
    }

    void Update()
    {
        
    }

    public void SpawnBlock()
    {
        Instantiate(blockToSpawn, transform.position, Quaternion.identity);
    }
}
