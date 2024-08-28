using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothChanger : MonoBehaviour
{
    public SkinnedMeshRenderer mesh;
    public Texture2D texture;

    public string shaferIdName = "_EmissionMap";

    [NaughtyAttributes.Button]
    private void ChangeTexture()
    {
        mesh.materials[0].SetTexture(shaferIdName, texture);
    }
}
