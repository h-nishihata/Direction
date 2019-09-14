using System;
using UnityEngine;

public class UISwitcher : MonoBehaviour
{
    public Transform UIPanel;
    private bool isEnabled;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            switchUIPanel(isEnabled = !isEnabled);
        }
    }

    public void switchUIPanel(bool isActive)
    {
        UIPanel.gameObject.SetActive(isActive);
    }

    public void Screenshot()
    {
        switchUIPanel(false);

        // 現在時刻からファイル名を決定
        var filename = System.DateTime.Now.ToString("/yyyyMMdd_HHmmss") + ".png";
        // キャプチャを撮る
        ScreenCapture.CaptureScreenshot(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + filename);
        switchUIPanel(true);
    }
}
