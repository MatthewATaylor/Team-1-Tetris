using UnityEngine;

public class AudioController : MonoBehaviour
{
    private Score score;
    private AudioSource audioSource;

    void Start()
    {
        score = GameObject.Find(GlobalNames.score).GetComponent<Score>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        audioSource.pitch = (1 - score.value / Score.maxValue) * 0.9f + 0.05f;
    }
}
