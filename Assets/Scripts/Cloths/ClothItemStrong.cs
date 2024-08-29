using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{

    public class ClothItemStrong : ClothItemBase
    {
        public float damageMultiplier = 2f;

        public override void Collect()
        {
            base.Collect();
            Player.Instance.healthBase.ChangeDamageMultiplier(damageMultiplier, clothChangeDuration);
        }
    }
}
