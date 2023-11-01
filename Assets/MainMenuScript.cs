using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainMenuScreen;
    public GameObject ExitConfirmOverlay;
    public GameObject creditsScreen;
    public GameObject settingsScreen;
    public GameObject controlsScreen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ExitConfirmOverlay.activeSelf)
            {
                ExitConfirmCancel();
            }
            else if (creditsScreen.activeSelf)
            {
                HideCredits();
            }
            else if (settingsScreen.activeSelf)
            {
                HideSettings();
            }
            else if (controlsScreen.activeSelf)
            {
                HideControls();
            }
        }
    }

    public void LoadScene(string levelSceneName)
    {
        if(levelSceneName != null)
        {
            SceneManager.LoadScene(levelSceneName);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    //credits screen
    public void ShowCredits()
    {
        creditsScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
    }

    public void HideCredits()
    {
        creditsScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }

    //settings screen
    public void ShowSettings()
    {
        settingsScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
    }

    public void HideSettings()
    {
        settingsScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
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

    public void RickRoll()
    {
        Application.OpenURL("https://youtu.be/xvFZjo5PgG0?si=8dOssogQQS_YhlaO");
    }

    public void ExitConfirm()
    {
        ExitConfirmOverlay.SetActive(true);
    }

    public void ExitConfirmCancel()
    {
        ExitConfirmOverlay.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
