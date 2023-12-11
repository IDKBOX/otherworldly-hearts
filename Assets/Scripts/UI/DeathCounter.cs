using TMPro;
using UnityEngine;

public class DeathCounter : MonoBehaviour
{
    [HideInInspector] public int DeathCount;
    public TextMeshProUGUI textDeath;

    private void Start()
    {
        loadDeathCount();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("DeathCount Reset");
            resetDeathCount();
            loadDeathCount();
        }
    }

    public void saveDeathCount(int count)
    {
        PlayerPrefs.SetInt("DeathCount", count);
    }

    public void loadDeathCount()
    {
        PlayerPrefs.GetInt("DeathCount", 0);
        textDeath.text = PlayerPrefs.GetInt("DeathCount").ToString();
    }

    public void resetDeathCount()
    {
        PlayerPrefs.SetInt("DeathCount", 0);
    }
}
