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
    public Transform dropPosition;

    private void OnValidate()
    {
        if (HealthBase == null) HealthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();
        HealthBase.onDamage += OnDamage;
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
        i.transform.position = dropPosition.position;
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
            yield return new WaitForSeconds(.25f);
        }
    }

}
