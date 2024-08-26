using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 01;
    private string checkpointKey = "CheckpointKey";
    private bool checkpointSaved = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!checkpointSaved &&other.transform.CompareTag("Player"))
        {
            CheckpointCheck();
        }
    }

    [NaughtyAttributes.Button]  
    private void CheckpointCheck()
    {
        TurnItOn();
        CheckpointManager.Instance.SaveCheckpoint(key);
    }

    private void TurnItOn()
    {
        if (meshRenderer != null) meshRenderer.material.SetColor("_EmissionColor", Color.white);
    }
    
    private void TurnItOff()
    {
        if (meshRenderer != null) meshRenderer.material.SetColor("_EmissionColor", Color.black);
    }

    private void SaveCheckpoint()
    {
        if(PlayerPrefs.GetInt(checkpointKey, 0) < key)
            PlayerPrefs.SetInt(checkpointKey, key);

        checkpointSaved = true;
    }

}
