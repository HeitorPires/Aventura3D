using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 01;
    private string checkpointKey = "CheckpointKey";
    public bool checkpointSaved = false;

    void Start()
    {
        Invoke(nameof(Init), .1f);
    }

    void Init()
    {
        if (SaveManager.Instance._saveSetup.checkpoints.Contains(key))
            CheckpointCheck();
    }


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
        if(CheckpointManager.Instance.SaveCheckpoint(key))
            SaveCheckpoint();
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
        checkpointSaved = true;
    }

}
