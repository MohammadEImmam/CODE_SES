using UnityEngine;

public class popupAnimation : MonoBehaviour
{
    public Animator animator;
    public GameObject panel;

    public void StartAnimation()
    {
        animator.enabled = true;
        animator.Play("popupAnimation", -1, 0f);
        panel.SetActive(true);
    }
}