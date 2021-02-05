using UnityEngine;

public class DropletSpawner : MonoBehaviour
{
    private const int particlesPerDrop = 50;
    private const int secondsPerDrop = 3;
    private const float particlesPerSecond = (float)particlesPerDrop / secondsPerDrop;

    [SerializeField] private GameObject dropletObject;

    private float lastDropTime = 0;
    private bool isDropping = false;
    private int numDroppedParticles = 0;
    private int numParticleLayers = 0;

    void Start()
    {

    }

    void Update()
    {
        // For debugging
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
                    droplet.transform.parent = transform;  // Set droplet spawner as parent
                    droplet.GetComponent<Renderer>().sortingOrder = -1;  // Render behind board
                    droplet.layer = GlobalNames.liquidLayer;  // Set layer for liquid camera
                }
                numDroppedParticles += numParticlesToDrop;

                // Calculate where foam should begin
                float adjustedNumParticleLayers = numParticleLayers + (float)numDroppedParticles / particlesPerDrop;
                float whiteHeight = 2 * (1 - adjustedNumParticleLayers / Score.maxLevel) - 0.8f;
                Shader.SetGlobalFloat("whiteHeight", whiteHeight);

                lastDropTime = Time.time;

                if (numDroppedParticles > particlesPerDrop)
                {
                    isDropping = false;
                    numDroppedParticles = 0;
                    ++numParticleLayers;
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
