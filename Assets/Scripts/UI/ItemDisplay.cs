using UnityEngine;

public class ItemDisplay : MonoBehaviour
{
    public static int itemsToShow;

    [Header("Prerequisites")]
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;

    private void Start()
    {
        itemsToShow = PlayerPrefs.GetInt("StoryItemData", -1);
    }

    void Update()
    {
        switch (itemsToShow)
        {
            default:
                item1.SetActive(false);
                item2.SetActive(false);
                item3.SetActive(false);
                break;
            case 1:
                item1.SetActive(true);
                item2.SetActive(false);
                item3.SetActive(false);
                break;
            case 2:
                item1.SetActive(true);
                item2.SetActive(true);
                item3.SetActive(false);
                break;
            case 3:
                item1.SetActive(true);
                item2.SetActive(true);
                item3.SetActive(true);
                break;
        }

        //debug, remove on release
/*        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("StoryItemDataDeleted");
            PlayerPrefs.DeleteKey("StoryItemData");
        }*/
    }
}
