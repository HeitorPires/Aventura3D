using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{

    public class ClothItemImortal : ClothItemBase
    {
        public float damageMultiplier = 0;

        public override void Collect()
        {
            base.Collect();
            Player.Instance.healthBase.ChangeDamageMultiplier(damageMultiplier, clothChangeDuration);
        }
    }
}
