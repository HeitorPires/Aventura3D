using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ChestItemCoin : ChestItemBase
{
    public int coinAmount = 5;
    public GameObject coinObject;
    public Vector2 randomRange = new Vector2(-2f, 2f);

    [Header("Animation")]
    public float animationDuration = .2f;
    public Ease ease = Ease.OutBack;

    private List<GameObject> _itens = new List<GameObject>();

    public override void ShowItem()
    {
        base.ShowItem();
        CreateItens();
    }

    [NaughtyAttributes.Button]
    private void CreateItens()
    {
        for (int i = 0; i < coinAmount; i++)
        {
            var item = Instantiate(coinObject);
            if (item.TryGetComponent<Collider>(out var collider))
                collider.enabled = false;
            Vector3 randomOffset = new Vector3(Random.Range(randomRange.x, randomRange.y), 0, Random.Range(randomRange.x, randomRange.y));
            item.transform.position = transform.position + randomOffset;
            //item.transform.position = transform.position + Random.Range(randomRange.x ,randomRange.y) * Vector3.forward + Random.Range(randomRange.x, randomRange.y) * Vector3.right;
            item.transform.DOScale(0, animationDuration).From().SetEase(ease);
            _itens.Add(item);
        }
    }

    [NaughtyAttributes.Button]
    public override void Collect()
    {
        base.Collect();
        foreach (var item in _itens)
        {
            item.transform.DOMoveY(2f, animationDuration).SetRelative();
            item.transform.DOScale(0, animationDuration / 2).SetDelay(animationDuration / 2);
            ItemManager.Instance.AddByType(ItemType.COIN);
        }
    }

}
