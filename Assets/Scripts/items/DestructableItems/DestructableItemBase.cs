using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestructableItemBase : MonoBehaviour
{
    public HealthBase HealthBase;

    [Header("Animation")]
    public float animationDuration = .2f;
    public Vector3 shakeStrenght = Vector3.up;
    public int shakeForce = 5;

    public int dropCoinsAmaount = 5;
    public GameObject coinPrefab;
    public List<Transform> dropPositions;

    private void OnValidate()
    {
        if (HealthBase == null) HealthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();
        HealthBase.onDamage += OnDamage;
        HealthBase.onKill += OnKill;
    }

    private void OnKill(HealthBase h)
    {
        if (TryGetComponent<Collider>(out var collider))
            collider.enabled = false;
    }

    private void OnDamage(HealthBase h)
    {
        transform.DOShakeScale(animationDuration, shakeStrenght, shakeForce);
        DropAmountOfCoins();
    }

    [NaughtyAttributes.Button]
    private void DropCoins()
    {
        var i = Instantiate(coinPrefab);
        i.transform.position = dropPositions[Random.Range(0, dropPositions.Count)].position;
        i.transform.DOScale(0, 1f).From().SetEase(Ease.OutBack);
    }

    [NaughtyAttributes.Button]
    private void DropAmountOfCoins()
    {
        StartCoroutine(DropAmountOfCoinsCoroutine());
    }

    IEnumerator DropAmountOfCoinsCoroutine()
    {
        for(int i = 0; i < dropCoinsAmaount; i++)
        {
            DropCoins();
            yield return new WaitForSeconds(.15f);
        }
    }

}
