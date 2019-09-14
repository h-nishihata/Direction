using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwitcher : MonoBehaviour
{
    public Transform UIPanel;
    private bool isEnabled;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            isEnabled = !isEnabled;
            switchUIPanel();
        }
    }

    public void switchUIPanel()
    {
        UIPanel.gameObject.SetActive(isEnabled);
    }
}
