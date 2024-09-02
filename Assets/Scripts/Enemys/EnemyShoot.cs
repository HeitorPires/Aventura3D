using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : EnemyBase
{
    public GunBase gun;
    public SFXType sFXType;

    protected override void Init()
    {
        base.Init();
        gun.StartShoot(sFXType);
    }

}
