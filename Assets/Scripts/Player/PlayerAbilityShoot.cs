using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{

    public List<GunBase> gunsBase;

    public Transform gunPosition;
    public FlashColor flashColor;

    public SFXType SfxSound;

    private GunBase _currentGun;

    protected override void Init()
    {
        base.Init();

        CreateGun();

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
        inputs.Gameplay.FirstWeapon.performed += ctx => SwitchGun(gunsBase[0]);
        inputs.Gameplay.SecondWeapon.performed += ctx => SwitchGun(gunsBase[1]);
    }

    private void CreateGun()
    {
        for(int i = 0; i <  gunsBase.Count; i++)
        {
            var a = Instantiate(gunsBase[i], gunPosition);
            a.transform.localPosition = a.transform.localEulerAngles = Vector3.zero;
            gunsBase[i] = a;
        }
        SwitchGun(gunsBase[0]);
    }

    private void SwitchGun(GunBase nextGun)
    {
        _currentGun = nextGun;
    }

    private void StartShoot()
    {
        _currentGun.StartShoot(SfxSound);
        flashColor?.Flash();
        ShakeCamera.Instance.Shake();
    }

    private void CancelShoot()
    {
        _currentGun.StopShoot();
    }

}
