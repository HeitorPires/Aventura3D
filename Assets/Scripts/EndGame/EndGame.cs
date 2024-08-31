using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndGame : MonoBehaviour
{
    public List<GameObject>  endGameObjects;

    [Header("Animation")]
    public float animationDuration = .2f;
    public Ease ease = Ease.OutBounce;

    public int currentLevel;

    private bool _endGame = false;

    private void Awake()
    {
        endGameObjects.ForEach(i => i.SetActive(false));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var a))
        {
            a.canMove = false;
            ShowEndGame(a.name);

        }
    }

    private void ShowEndGame(string name)
    {
        _endGame = true;
        foreach (var i in endGameObjects)
        {
            i.SetActive(true);
            //i.transform.DOScale(0, animationDuration).SetEase(ease).From();
        }
        SaveManager.Instance.HandleSave(currentLevel, name);

    }
}
