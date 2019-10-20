using System;
using UnityEngine;

public class UISwitcher : MonoBehaviour
{
    public Transform UIPanel;
    private bool UIActive = true;
    private bool shutterPressed;
    private float waitForScreenshot;
    public Texture2D screenShot;

    private void Start()
    {
        screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }

    private void Update()
    {
        if (!UIActive && Input.GetMouseButtonDown(0))
            switchUIPanel(true);
    }

    public void switchUIPanel(bool isActive)
    {
        UIPanel.gameObject.SetActive(isActive);
        UIActive = isActive;
    }

    public void Screenshot()
    {
        if (shutterPressed)
            return;

        switchUIPanel(false);
        shutterPressed = true;
    }

    private void OnPostRender()
    {
        if (shutterPressed)
        {
            if (waitForScreenshot < 1.0f)
                waitForScreenshot += Time.deltaTime;
            else
            {
                // 現在時刻からファイル名を決定
                var fileName = System.DateTime.Now.ToString("/yyyyMMdd_HHmmss") + ".png";
                // キャプチャを撮る
                screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
                screenShot.Apply();
                NativeGallery.SaveImageToGallery(screenShot, "album", fileName);

                shutterPressed = false;
                waitForScreenshot = 0f;
                switchUIPanel(true);
            }
        }
    }
}
