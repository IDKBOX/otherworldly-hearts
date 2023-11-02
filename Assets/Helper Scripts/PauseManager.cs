using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    private GameObject pauseFirstButton;
    public GameObject mainMenuConfirmOverlay;
    [HideInInspector] public static bool isPaused;
    public Animator transition;
    public GameObject settingsScreen;
    public GameObject controlsScreen;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mainMenuConfirmOverlay.activeSelf)
            {
                CancelReturnToTitleScreen();
            }
            else
            {
                PausePressed();
            }
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

        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
/*        //set a new selected object
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);*/
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ConfirmReturnToTitleScreen()
    {
        mainMenuConfirmOverlay.SetActive(true);
    }

    public void CancelReturnToTitleScreen()
    {
        mainMenuConfirmOverlay.SetActive(false);
    }

    public void ReturnToTitleScreen()
    {
        StartCoroutine(StartTransition());
    }

    IEnumerator StartTransition()
    { 
        transition.SetTrigger("StartTransition");
        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(0);
    }

    //settings screen
    public void ShowSettings()
    {
        settingsScreen.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void HideSettings()
    {
        settingsScreen.SetActive(false);
        pauseMenu.SetActive(true);
    }

    //controls screen
    public void ShowControls()
    {
        settingsScreen.SetActive(false);
        controlsScreen.SetActive(true);
    }

    public void HideControls()
    {
        settingsScreen.SetActive(true);
        controlsScreen.SetActive(false);
    }
}
