using System.Collections;
using UnityEngine;

public class UISwitcher : MonoBehaviour
{
    public Transform UIPanel;
    private bool UIActive = true;
    private bool shutterPressed;
    private bool flashStarted;
    private float waitForScreenshot = 1.0f;
    public Renderer flash;
    private Material flashMaterial;
    public Texture2D screenShot;

    private void Start()
    {
        screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
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
            shutterPressed = false;
        }

        if (flashStarted)
        {
            if (waitForScreenshot > 0.0f)
            {
                waitForScreenshot -= 0.02f;
                flashMaterial.SetFloat("_Blend", waitForScreenshot);
            }
            else
            {
                flashStarted = false;
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
    }

    /// <summary>
    /// iOSデバイスのPhotosアプリにスクリーンショットを保存する.
    /// </summary>
    private IEnumerator TakeScreenshotAndSave()
    {
        yield return new WaitForEndOfFrame();

        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();

        // 現在時刻からファイル名を決定
        //var fileName = System.DateTime.Now.ToString("/yyMMdd_HHmmss") + ".png";
        //Debug.Log(fileName);
        // Save the screenshot to Gallery/Photos
        Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(screenShot, "Direction", "Image.png"));

        flashStarted = true;
        flashMaterial.SetFloat("_Blend", waitForScreenshot);
        // To avoid memory leaks
        //Destroy(screenShot);
    }
}
