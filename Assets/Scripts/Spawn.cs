using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject[] tetrads;

    void Start()
    {
        SpawnBlock();
    }

    void Update()
    {
        
    }

    public void SpawnBlock()
    {
        GameObject newBlock = Instantiate(tetrads[Random.Range(0, tetrads.Length)], transform.position, Quaternion.identity);
        newBlock.transform.SetParent(transform.parent);  // Set parent to Playboard
    }
}
