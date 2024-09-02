using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;

    public Transform positionToShoot;
    public float timeBetweenShoot = .2f;
    public float speed = 50f;

    public KeyCode shootKeyCode = KeyCode.Q;

    private Coroutine _currentCoroutine;


    protected virtual IEnumerator ShootCoroutine(SFXType sFXSound)
    {
        while (true)
        {
            Shoot();
            SFXPool.Instance.Play(sFXSound);
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    protected virtual void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
        projectile.speed = speed;

    }

    public void StartShoot(SFXType sFXSound)
    {
        StopShoot();
        _currentCoroutine = StartCoroutine(ShootCoroutine(sFXSound));
    }

    public void StopShoot()
    {
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
    }

    

}
