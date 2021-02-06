using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(GlobalNames.titleScene);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(GlobalNames.mainScene);
        }
    }
}
