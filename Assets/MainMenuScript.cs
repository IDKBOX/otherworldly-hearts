using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject ExitConfirmOverlay;

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
