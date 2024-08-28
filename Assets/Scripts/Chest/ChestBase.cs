using DG.Tweening;
using UnityEngine;

public class ChestBase : MonoBehaviour
{

    public Animator Animator;
    public string triggerOpen = "Open";
    public string tagToCompare = "Player";
    public GameObject notification;

    [Header("Animation")]
    public float animationDuration = 1f;
    public Ease ease = Ease.OutBounce;
    public float startScale;

    // Start is called before the first frame update
    void Start()
    {
        HideNotification();
        startScale = notification.transform.localScale.x;
    }

    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        Animator.SetTrigger(triggerOpen);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(tagToCompare))
        {
            ShowNotification();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag(tagToCompare))
        {
            HideNotification();
        }
    }

    [NaughtyAttributes.Button]
    private void ShowNotification()
    {
        notification.SetActive(true);
        notification.transform.localScale = Vector3.zero;
        notification.transform.DOScale(startScale, animationDuration).SetEase(ease);
    }

    [NaughtyAttributes.Button]
    private void HideNotification()
    {
        notification.SetActive(false);
    }
}
