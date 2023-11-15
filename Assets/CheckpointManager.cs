using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;
    public Vector2 lastCheckPointPos;
    public string sceneActive;

    private void Awake()
    {
        GameObject startPosition = GameObject.FindGameObjectWithTag("SpawnPoint");
        lastCheckPointPos = startPosition.transform.position;

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
        GameObject startPosition = GameObject.FindGameObjectWithTag("SpawnPoint");
        lastCheckPointPos = startPosition.transform.position;
        FindAnyObjectByType<RespawnPlayer>().gameObject.transform.position = lastCheckPointPos;
    }
}
