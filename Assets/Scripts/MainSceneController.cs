using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMessage;
    [SerializeField] private GameObject gameOverMessage;
    [SerializeField] private AudioSource plinkSource;

    void Start()
    {
        
    }

    void Update()
    {
        pauseMessage.SetActive(GlobalState.IsPaused);
        gameOverMessage.SetActive(GlobalState.IsGameOver);

        if (!GlobalState.IsGameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            GlobalState.IsPaused = !GlobalState.IsPaused;
            plinkSource.Play();
        }

        if (GlobalState.IsPaused || GlobalState.IsGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                GlobalState.IsPaused = false;
                GlobalState.IsGameOver = false;
                SceneManager.LoadScene(GlobalNames.mainScene);
                plinkSource.Play();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                GlobalState.IsPaused = false;
                GlobalState.IsGameOver = false;
                SceneManager.LoadScene(GlobalNames.titleScene);
                plinkSource.Play();
            }
        }

    }
}
