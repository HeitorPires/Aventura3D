using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartCheck : MonoBehaviour
{
    public string tagToCheck = "Player";
    public Color color = Color.yellow;

    public GameObject bossCamera;

    private void Awake()
    {
        bossCamera.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(tagToCheck))
        {
            TurnCamercaOn();
        }
    }

    private void TurnCamercaOn()
    {
        bossCamera.SetActive(true);    
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, transform.localScale.y);
    }
}
