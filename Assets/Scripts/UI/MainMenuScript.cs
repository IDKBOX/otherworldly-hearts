using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainMenuScreen;
    public GameObject ExitConfirmOverlay;
    public GameObject NewGameConfirmOverlay;
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
            else if (NewGameConfirmOverlay.activeSelf)
            {
                NewGameConfirmCancel();
            }
        }
    }

    public void LoadScene(string levelSceneName)
    {
        StartCoroutine(StartTransition(levelSceneName));
        SoundManager.Instance.FadeOut();
    }

    IEnumerator StartTransition(string levelSceneName)
    {
        TransitionManager.Instance.StartTransition();

        yield return new WaitForSeconds(1f);
        
        TransitionManager.Instance.EndTransition();

        if (levelSceneName != null && levelSceneName != "001_Intro")
        {
            SceneManager.LoadScene("Base");
            SceneManager.LoadScene(levelSceneName, LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene(levelSceneName);
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

    //exit confirm overlay
    public void ExitConfirm()
    {
        ExitConfirmOverlay.SetActive(true);
    }

    public void ExitConfirmCancel()
    {
        ExitConfirmOverlay.SetActive(false);
    }

    //new game confirm overlay
    public void NewGameConfirm()
    {
        NewGameConfirmOverlay.SetActive(true);
    }

    public void NewGameConfirmCancel()
    {
        NewGameConfirmOverlay.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
