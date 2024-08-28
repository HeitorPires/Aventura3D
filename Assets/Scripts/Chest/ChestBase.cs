using DG.Tweening;
using UnityEngine;

public class ChestBase : MonoBehaviour
{

    public Animator Animator;
    public string triggerOpen = "Open";
    public string tagToCompare = "Player";
    public GameObject notification;
    public KeyCode keyCode = KeyCode.F;

    [Header("Animation")]
    public float animationDuration = 1f;
    public Ease ease = Ease.OutBounce;
    public float startScale;

    [Space]
    public ChestItemBase chestItem; 

    private bool _isChestOpened;

    // Start is called before the first frame update
    void Start()
    {
        HideNotification();
        startScale = notification.transform.localScale.x;
    }

    private void Update()
    {
        if(Input.GetKeyDown(keyCode))
        {
            OpenChest();
        }
    }

    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        if (_isChestOpened) return;
        Animator.SetTrigger(triggerOpen);
        _isChestOpened = true;
        HideNotification();
        Invoke(nameof(ShowItem), 1f);
        Invoke(nameof(CollectItems), 2f);
    }

    private void ShowItem()
    {
        chestItem.ShowItem();
    }
    
    private void CollectItems()
    {
        chestItem.Collect();
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
        if (_isChestOpened) return;
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
