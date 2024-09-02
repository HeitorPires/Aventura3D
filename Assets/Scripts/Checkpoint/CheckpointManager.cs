using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckpointKey = 0;
    public List<CheckpointBase> checkpoints;

    public bool HasCheckPoint()
    {
        return lastCheckpointKey > 0;
    }

    public bool SaveCheckpoint(int i)
    {
        if (i > lastCheckpointKey)
        {
            lastCheckpointKey = i;
            return true;
        }
        return false;
    }

    public Vector3 GetPositionFromLastCheckpoint()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckpointKey);
        return checkpoint.transform.position;
    }
}
