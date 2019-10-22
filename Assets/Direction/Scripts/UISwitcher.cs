using System.Collections;
using UnityEngine;

public class UISwitcher : MonoBehaviour
{
    public Transform UIPanel;
    private bool UIActive = true;
    private bool shutterPressed;
    private float waitForScreenshot = 1.0f;
    public Renderer flash;
    private Material flashMaterial;


    private void Start()
    {
        flashMaterial = flash.material;
    }

    private void Update()
    {
        // UIパネル非表示時に，画面をタップすると再度表示する.
        if (!UIActive && Input.GetMouseButtonDown(0))
            switchUIPanel(true);

        if (shutterPressed)
        {
            StartCoroutine(TakeScreenshotAndSave());

            if (waitForScreenshot > 0.0f)
            {
                waitForScreenshot -= 0.1f;
                flashMaterial.SetFloat("_Blend", waitForScreenshot);
            }
            else
            {
                // 現在時刻からファイル名を決定
                //var fileName = System.DateTime.Now.ToString("/yyyyMMdd_HHmmss") + ".png";
                // キャプチャを撮る
                //screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
                //screenShot.Apply();
                //NativeGallery.SaveImageToGallery(screenShot, "album", fileName);

                shutterPressed = false;
                waitForScreenshot = 1f;
                switchUIPanel(true);
            }

        }
    }

    /// <summary>
    /// UIパネルの表示/非表示の切替をする.
    /// </summary>
    public void switchUIPanel(bool isActive)
    {
        UIPanel.gameObject.SetActive(isActive);
        UIActive = isActive;
    }

    /// <summary>
    /// 「Screenshot」ボタンの押下を受け付ける.
    /// </summary>
    public void Screenshot()
    {
        if (shutterPressed)
            return;

        switchUIPanel(false);
        shutterPressed = true;
        flashMaterial.SetFloat("_Blend", waitForScreenshot);
    }

    /// <summary>
    /// iOSデバイスのPhotosアプリにスクリーンショットを保存する.
    /// </summary>
    private IEnumerator TakeScreenshotAndSave()
    {
        yield return new WaitForEndOfFrame();

        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();

        // 現在時刻からファイル名を決定
        var fileName = System.DateTime.Now.ToString("/yyyyMMdd_HHmmss") + ".png";

        // Save the screenshot to Gallery/Photos
        Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(screenShot, "Direction", fileName));

        // To avoid memory leaks
        Destroy(screenShot);
    }
}
