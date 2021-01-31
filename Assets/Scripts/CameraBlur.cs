using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraBlur : MonoBehaviour
{
    private DepthOfField depthOfField;
    private Score score;

    void Start()
    {
        PostProcessVolume volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out depthOfField);

        score = GameObject.Find(GlobalNames.score).GetComponent<Score>();
    }

    void Update()
    {
        depthOfField.focalLength.value = 300.0f * score.GetProgress();
    }
}
