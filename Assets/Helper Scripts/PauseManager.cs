using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseFirstButton;
    [HideInInspector] public static bool isPaused;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PausePressed();
        }
    }

    public void PausePressed()
    {
        if (!isPaused)
        {
            PauseGame();
        }
        else if (isPaused)
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

/*        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);*/
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ReturnToTitleScreen()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(0);
    }
}
