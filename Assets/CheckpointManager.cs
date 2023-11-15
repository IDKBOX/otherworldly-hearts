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
        Debug.Log("checkpointReset");
        checkpointActive = false;
    }
}
