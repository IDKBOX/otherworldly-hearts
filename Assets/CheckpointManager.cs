using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private static CheckpointManager instance;
    public Vector2 lastCheckPointPos;
    public string sceneActive;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
