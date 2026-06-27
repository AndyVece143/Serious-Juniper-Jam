using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public LevelLoader loader;
    public GameObject pauseMenuUI;
    public GameObject panel;
    public GameObject gameUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        loader = LevelLoader.FindAnyObjectByType<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        //panel.SetActive(false);
        if (gameUI)
        {
            gameUI.SetActive(true);
        }
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        //panel.SetActive(true);
        if (gameUI)
        {
            gameUI.SetActive(false);
        }
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void RestartLevel()
    {
        Resume();
        string currentSceneName = SceneManager.GetActiveScene().name;
        loader.LoadNextLevel(currentSceneName);
    }

    public void QuitToMenu()
    {
        Resume();
        loader.LoadNextLevel("Title");
    }
}
