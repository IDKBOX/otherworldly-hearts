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
            loadCheckpointData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //for testing, remove on release!
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("CheckpointData Deleted");
            deleteCheckpointData();
            loadCheckpointData();
        }
    }

    public void resetCheckpoint()
    {
        checkpointActive = false;
    }

    //save system
    public void saveLevel()
    {
        PlayerPrefs.SetString("levelData", sceneActive);
        PlayerPrefs.SetInt("checkpointActive", 0);
        PlayerPrefs.SetFloat("checkpointX", lastCheckPointPos.x);
        PlayerPrefs.SetFloat("checkpointY", lastCheckPointPos.y);
    }

    public void saveCheckpoint()
    {
        PlayerPrefs.SetString("levelData", sceneActive);
        PlayerPrefs.SetInt("checkpointActive", 1);
        PlayerPrefs.SetFloat("checkpointX", lastCheckPointPos.x);
        PlayerPrefs.SetFloat("checkpointY", lastCheckPointPos.y);
    }

    public void loadCheckpointData()
    {
        if (PlayerPrefs.GetInt("checkpointActive", 0) == 0)
        {
            checkpointActive = false;
        }
        else
        {
            checkpointActive = true;
        }

        lastCheckPointPos.x = PlayerPrefs.GetFloat("checkpointX", 0);
        lastCheckPointPos.y = PlayerPrefs.GetFloat("checkpointY", 0);
    }

    public void deleteCheckpointData()
    {
        PlayerPrefs.DeleteKey("levelData");
        PlayerPrefs.DeleteKey("checkpointActive");
        PlayerPrefs.DeleteKey("checkpointX");
        PlayerPrefs.DeleteKey("checkpointY");
    }
}
