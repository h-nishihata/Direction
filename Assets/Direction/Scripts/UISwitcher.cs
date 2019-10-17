using System;
using UnityEngine;

public class UISwitcher : MonoBehaviour
{
    public Transform UIPanel;
    public BoxCollider collider;
    private Vector3 colCenterHalf = new Vector3(-2.5f, 0f, 0f);
    private Vector3 colSizeHalf = new Vector3(5f, 3f, 10f);
    private Vector3 colSizeFull = new Vector3(10f, 3f, 10f);
    private bool isEnabled = true;
    private bool shutterPressed;
    private float waitForScreenshot;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("hit");
                switchUIPanel(isEnabled = !isEnabled);
                if(isEnabled)
                {
                    collider.center = colCenterHalf;
                    collider.size = colSizeHalf;
                }
                else if(!isEnabled)
                {
                    collider.center = Vector3.zero;
                    collider.size = colSizeFull;
                }
            }
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
