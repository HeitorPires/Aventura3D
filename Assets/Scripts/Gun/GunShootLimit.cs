using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GunShootLimit : GunBase
{

    public string uiAmmoName = "UIGunAmmo";

    public List<UIFillUpdater> uIGunUpdaters;

    public float maxShoot = 5;
    public float timeToRecharge = 1f;

    private float _currentShoots;

    private bool _recharging = false;


    private void Awake()
    {
        GetAllUIs();
    }

    protected override IEnumerator ShootCoroutine()
    {

        if (_recharging) yield break;

        while (true)
        {
            if (_currentShoots < maxShoot)
            {
                Shoot();
                _currentShoots++;
                CheckRecharge();
                UpdateUI();
                yield return new WaitForSeconds(timeBetweenShoot);
            }

        }
    }

    private void CheckRecharge()
    {
        if (_currentShoots >= maxShoot)
        {
            StopShoot();
            StartRecharge();
        }
    }

    private void StartRecharge()
    {
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0;
        while (time < timeToRecharge)
        {
            time += Time.deltaTime;
            uIGunUpdaters.ForEach(i => i.UpdateValue(time / timeToRecharge));
            yield return new WaitForEndOfFrame();
        }
        _currentShoots = 0;
        _recharging = false;
    }

    private void UpdateUI()
    {
        uIGunUpdaters.ForEach(i => i.UpdateValue(maxShoot, _currentShoots));
    }

    private void GetAllUIs()
    {
        var uIs = GameObject.FindObjectsOfType<UIFillUpdater>().ToList();
        foreach (var u in uIs)
        {
            if (u.name == uiAmmoName)
                uIGunUpdaters.Add(u);

        }
    }
}

    
