using UnityEngine;

public class DeleteSaveDataOnStart : MonoBehaviour
{
    void Start()
    {
        CheckpointManager.Instance.deleteCheckpointData();
        CheckpointManager.Instance.loadCheckpointData();
        PlayerPrefs.DeleteKey("StoryItemData");
    }
}
