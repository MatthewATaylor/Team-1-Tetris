using UnityEngine;

public class DropletSpawner : MonoBehaviour
{
    private const int particlesPerDrop = 60;
    private const int secondsPerDrop = 3;
    private const float particlesPerSecond = (float)particlesPerDrop / secondsPerDrop;

    [SerializeField] private GameObject dropletObject;

    private float lastDropTime = 0;
    private bool isDropping = false;
    private int numDroppedParticles = 0;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartDrop();
        }

        if (isDropping)
        {
            int numParticlesToDrop = Mathf.RoundToInt((Time.time - lastDropTime) * particlesPerSecond);
            if (numParticlesToDrop > 0)
            {
                for (int i = 0; i < numParticlesToDrop; ++i)
                {
                    Vector2 dropletPosition = transform.position;
                    dropletPosition.x += Random.Range(-0.5f, 0.5f);
                    dropletPosition.y += Random.Range(-0.5f, 0.5f);
                    GameObject droplet = Instantiate(dropletObject, dropletPosition, Quaternion.identity);
                    droplet.transform.parent = transform;
                    droplet.GetComponent<Renderer>().sortingOrder = 0;
                    droplet.layer = GlobalNames.liquidLayer;
                }
                StaticBatchingUtility.Combine(transform.gameObject);
                numDroppedParticles += numParticlesToDrop;

                lastDropTime = Time.time;

                if (numDroppedParticles > particlesPerDrop)
                {
                    isDropping = false;
                    numDroppedParticles = 0;
                }
            }
        }
    }

    public void StartDrop()
    {
        lastDropTime = Time.time;
        isDropping = true;
    }
}
