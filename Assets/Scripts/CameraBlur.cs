using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraBlur : MonoBehaviour
{
    private Blurred blurred;
    private Score score;

    void Start()
    {
        PostProcessVolume volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out blurred);

        score = GameObject.Find(GlobalNames.score).GetComponent<Score>();
    }

    void Update()
    {
        blurred.maxOffset.value = (int)(score.GetProgress() * 120);
    }
}
