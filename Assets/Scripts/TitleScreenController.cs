using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{
    [SerializeField] private GameObject startMask;
    [SerializeField] private GameObject creditsMask;

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
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!startMask.activeInHierarchy)
            {
                SceneManager.LoadScene(GlobalNames.mainScene);
            }
        }
    }
}
