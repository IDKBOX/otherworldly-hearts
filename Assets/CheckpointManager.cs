using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;
    public Vector2 lastCheckPointPos;
    public string sceneActive;
    [HideInInspector] public bool checkpointActive;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void resetCheckpoint()
    {
        checkpointActive = false;
        saveLevel();
    }

    //save system
    public void saveLevel()
    {
        PlayerPrefs.SetString("levelData", sceneActive);
        PlayerPrefs.SetInt("checkpointActive", 0);
    }

    public void saveCheckpoint()
    {
        PlayerPrefs.SetString("levelData", sceneActive);
        PlayerPrefs.SetInt("checkpointActive", 1);
        PlayerPrefs.SetFloat("checkpointX", lastCheckPointPos.x);
        PlayerPrefs.SetFloat("checkpointY", lastCheckPointPos.y);
    }
}
