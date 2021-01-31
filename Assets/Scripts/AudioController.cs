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
        // Decrease music pitch and tempo as score increases
        audioSource.pitch = (1 - score.GetProgress()) * 0.9f + 0.05f;
    }
}
