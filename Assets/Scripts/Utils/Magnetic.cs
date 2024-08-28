using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    public float dist = .3f;
    public float coinSpeed = 3f;

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, Player.Instance.transform.position) > dist)
        {
            coinSpeed++;
            transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, coinSpeed * Time.deltaTime);
        }
    }
}
