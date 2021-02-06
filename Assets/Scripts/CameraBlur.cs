using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraBlur : MonoBehaviour
{
    [SerializeField] private Transform boardTopLeft;
    [SerializeField] private Transform boardBottomRight;

    private Camera cam;
    private Blurred blurred;
    private Score score;

    void Start()
    {
        cam = GetComponent<Camera>();

        PostProcessVolume volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out blurred);

        score = GameObject.Find(GlobalNames.score).GetComponent<Score>();
    }

    void Update()
    {
        blurred.maxOffset.value = (int)(score.GetProgress() * 120);

        Vector3 topLeft = cam.WorldToScreenPoint(boardTopLeft.position);
        Vector3 bottomRight = cam.WorldToScreenPoint(boardBottomRight.position);

        blurred.boardLeft.value = topLeft.x / Screen.width;
        blurred.boardRight.value = bottomRight.x / Screen.width;
        blurred.boardTop.value = topLeft.y / Screen.height;
        blurred.boardBottom.value = bottomRight.y / Screen.height;

        blurred.screenWidth.value = Screen.width;
        blurred.screenHeight.value = Screen.height;
    }
}
