using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHelper : MonoBehaviour
{

    public Button playButton;
    public Button loadButton;

    private void Awake()
    {
        // Assume que SaveManager tem um método público para Play e Load
        SaveManager saveManager = FindObjectOfType<SaveManager>();

        if (saveManager != null)
        {
            playButton.onClick.AddListener(saveManager.CreateNewEmptyGame);
            loadButton.onClick.AddListener(saveManager.Load);
        }
    }

}
