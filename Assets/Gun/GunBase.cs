using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;

    public Transform positionToShoot;
    public float timeBeteenShoot = .2f;

    public KeyCode shootKeyCode = KeyCode.Q;

    private Coroutine _currentCoroutine;

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(shootKeyCode))
        {
            _currentCoroutine = StartCoroutine(StartShoot());
        }
        else if (Input.GetKeyUp(shootKeyCode))
        {
            if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        }
    }

    IEnumerator StartShoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBeteenShoot);
        }
    }

    public void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
    }

    

}
