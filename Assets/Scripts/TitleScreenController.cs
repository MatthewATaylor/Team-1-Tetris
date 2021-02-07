using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{
    [SerializeField] private GameObject startMask;
    [SerializeField] private GameObject creditsMask;
    [SerializeField] private AudioSource plinkSource;
    [SerializeField] private GameObject controlsMessage;

    void Start()
    {
        startMask.SetActive(false);
        creditsMask.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            startMask.SetActive(!startMask.activeInHierarchy);
            creditsMask.SetActive(!creditsMask.activeInHierarchy);

            plinkSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!startMask.activeInHierarchy)
            {
                SceneManager.LoadScene(GlobalNames.mainScene);
            }

            plinkSource.Play();
        }

        // Display controls when controls text is selected
        controlsMessage.SetActive(!creditsMask.activeInHierarchy);
    }
}
