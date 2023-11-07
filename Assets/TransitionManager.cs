using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;
    public Animator animator;

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

        animator = GetComponentInChildren<Animator>();
    }

    public void StartTransition()
    {
        animator.SetTrigger("StartTransition");
    }

    public void EndTransition()
    {
        animator.SetTrigger("EndTransition");
    }
}
