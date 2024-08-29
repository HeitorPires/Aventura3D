using Cloth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 10f;
    [SerializeField] private float _currentLife;

    public List<UIFillUpdater> uiHealthUpdater;


    public Action<HealthBase> onDamage;
    public Action<HealthBase> onKill;

    public bool destroyOnKill = true;
    public float timeToDestroy = 3f;

    public float damageMultiplier = 1f;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        ResetLife();
    }

    public void ResetLife()
    {
        _currentLife = startLife;
        UpdateUI();
    }

    protected virtual void Kill()
    {
        onKill?.Invoke(this);
        if(destroyOnKill) Destroy(gameObject, timeToDestroy);
    }

    public void Damage(float damage)
    {
        _currentLife -= damage * damageMultiplier;
        UpdateUI();
        onDamage?.Invoke(this);
        if (_currentLife <= 0)
            Kill();
    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }

    private void UpdateUI()
    {
        if(uiHealthUpdater != null)
        {
            uiHealthUpdater.ForEach(i => i.UpdateValue((float)_currentLife/startLife));
        }
    }

    public void ChangeDamageMultiplier(float damageMultiplier, float duration)
    {
        StartCoroutine(ChangeDamageMultiplierCoroutine(damageMultiplier, duration));
    }

    IEnumerator ChangeDamageMultiplierCoroutine(float damageMultiplier, float duration)
    {
        this.damageMultiplier = damageMultiplier;
        yield return new WaitForSeconds(duration);
        this.damageMultiplier = 1;
    }
}
