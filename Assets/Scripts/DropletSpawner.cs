using UnityEngine;

public class DropletSpawner : MonoBehaviour
{
    private const int maxLevel = 8;
    private const int particlesPerDrop = 40;
    private const int secondsPerDrop = 3;
    private const float particlesPerSecond = (float)particlesPerDrop / secondsPerDrop;

    [SerializeField] private GameObject dropletObject;
    [SerializeField] private GameObject bottomIndicator;
    [SerializeField] private Camera liquidCamera;

    private float top;
    private float bottom;

    private float normalizedTop;
    private float normalizedBottom;

    private float lastDropTime = 0;
    private bool isDropping = false;
    private int numDroppedParticles = 0;
    private int numParticleLayers = 0;

    void Start()
    {
        // Find top and bottom of beer container
        top = transform.position.y;

        float bottomIndicatorHeight = 0;
        if (bottomIndicator.TryGetComponent(out Collider2D bottomCollider))
        {
            bottomIndicatorHeight = bottomCollider.bounds.size.y;
        }
        bottom = bottomIndicator.transform.position.y + bottomIndicatorHeight / 2.0f;

        // Find location of top and bottom in HLSL screen coordinates
        float cameraViewHalfHeight = liquidCamera.orthographicSize;
        normalizedTop = top / cameraViewHalfHeight * -1;
        normalizedBottom = bottom / cameraViewHalfHeight * -1;
    }

    void Update()
    {
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
                    droplet.GetComponent<Renderer>().sortingOrder = -2;  // Render behind board
                    droplet.layer = GlobalNames.liquidLayer;  // Set layer for liquid camera
                }
                numDroppedParticles += numParticlesToDrop;

                // Calculate where foam should begin
                float adjustedNumParticleLayers = numParticleLayers + (float)numDroppedParticles / particlesPerDrop;
                float fillFraction = adjustedNumParticleLayers / maxLevel;  // 0 to 1
                float fillTop = normalizedBottom * (1 - fillFraction) + normalizedTop * fillFraction;  // normalizedBottom to normalizedTop
                Shader.SetGlobalFloat("whiteHeight", fillTop + 0.06f);

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
