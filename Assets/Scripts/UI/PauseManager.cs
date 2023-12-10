using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.Audio;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject mainMenuConfirmOverlay;
    [HideInInspector] public static bool isPaused;
    public GameObject settingsScreen;
    public GameObject controlsScreen;
    public GameObject dialogueCanvas;
    public GameObject dialogueUI;
    [HideInInspector] public static bool canPause = true;

    //audio lowpass filter
    public AudioMixerSnapshot normalAudioSnapshot;
    public AudioMixerSnapshot lowpassAudioSnapshot;

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
            else if (settingsScreen.activeSelf)
            {
                HideSettings();
            }
            else if (controlsScreen.activeSelf)
            {
                HideControls();
            }
            else
            {
                PausePressed();
            }
        }
    }

    public void PausePressed()
    {
        if (canPause)
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
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        lowpassAudioSnapshot.TransitionTo(0f);
        Time.timeScale = 0f;
        isPaused = true;
        FindObjectOfType<CharacterMovement>().isDisabled = true;
        MobileUIManager.Instance.hideMobileUI();

        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        if (dialogueUI.activeSelf != true)
        {
            FindObjectOfType<CharacterMovement>().isDisabled = false;
        }
        MobileUIManager.Instance.showMobileUI();
        normalAudioSnapshot.TransitionTo(0f);
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
        SoundManager.Instance.FadeOut();
        SoundManager.Instance.isLevelMusicPlaying = false;
        Destroy(dialogueCanvas);
    }

    IEnumerator StartTransition()
    {
        TransitionManager.Instance.StartTransition();
        canPause = false;

        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(0);
        TransitionManager.Instance.EndTransition();
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
