using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagneticTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<CollectableItemBase>(out var i))
        {
            i.gameObject.AddComponent<Magnetic>();
        }
    }
}
