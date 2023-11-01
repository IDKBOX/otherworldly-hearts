using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
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

    public void ExitButton()
    {
        Application.Quit();
    }
}
