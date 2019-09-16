using System;
using UnityEngine;

public class UISwitcher : MonoBehaviour
{
    public Transform UIPanel;
    private bool isEnabled;
    private bool shutterPressed;
    private float waitForScreenshot;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            switchUIPanel(isEnabled = !isEnabled);
        }

        if (shutterPressed)
        {
            if (waitForScreenshot < 1.0f)
                waitForScreenshot += Time.deltaTime;
            else
            {
                shutterPressed = false;
                waitForScreenshot = 0f;
                switchUIPanel(true);
            }
        }
    }

    public void switchUIPanel(bool isActive)
    {
        UIPanel.gameObject.SetActive(isActive);
    }

    public void Screenshot()
    {
        if (shutterPressed)
            return;

        switchUIPanel(false);

        // 現在時刻からファイル名を決定
        var filename = System.DateTime.Now.ToString("/yyyyMMdd_HHmmss") + ".png";
        // キャプチャを撮る
        ScreenCapture.CaptureScreenshot(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + filename);

        shutterPressed = true;
    }
}
