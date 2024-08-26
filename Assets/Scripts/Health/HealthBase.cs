using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 10f;
    [SerializeField] private float _currentLife;
   
    public Action<HealthBase> onDamage;
    public Action<HealthBase> onKill;

    public bool destroyOnKill = true;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        ResetLife();
    }

    protected virtual void ResetLife()
    {
        _currentLife = startLife;
    }

    protected virtual void Kill()
    {
        onKill?.Invoke(this);
        if(destroyOnKill) Destroy(gameObject, 3f);
    }

    public void Damage(float damage)
    {
        _currentLife -= damage;
        onDamage?.Invoke(this);
        if (_currentLife <= 0)
            Kill();
    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }
}
