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
        Instantiate(tetrads[Random.Range(0, tetrads.Length)], transform.position, Quaternion.identity);
    }
}
