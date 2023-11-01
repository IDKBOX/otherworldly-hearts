using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject MainMenuOverlay;
    public GameObject ExitConfirmOverlay;
    public GameObject CreditsOverlay;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ExitConfirmOverlay.activeSelf)
            {
                ExitConfirmCancel();
            }
            else if (CreditsOverlay.activeSelf)
            {
                HideCredits();
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

    public void ShowCredits()
    {
        CreditsOverlay.SetActive(true);
        MainMenuOverlay.SetActive(false);
    }

    public void HideCredits()
    {
        CreditsOverlay.SetActive(false);
        MainMenuOverlay.SetActive(true);
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
