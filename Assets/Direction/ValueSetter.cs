using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ValueSetter : MonoBehaviour
{
    public Material[] mats;
    public Slider[] sliders;

    public ColorPicker picker;

    public Color lineColor = Color.black;


    void Start()
    {
        picker.onValueChanged.AddListener(color =>
        {
            lineColor = color;
        });
        picker.CurrentColor = lineColor;
    }

    void Update()
    {
        // パターン作成.
        mats[0].SetFloat("_Frequency", sliders[0].value);
        mats[0].SetFloat("_Fill", sliders[1].value);
        mats[0].SetColor("_LineColor", lineColor);
        mats[0].SetColor("_BGColor", Color.cyan);

        // マスク作成.
        for (int i = 1; i < mats.Length; i++)
        {
            mats[i].SetFloat("_Frequency", sliders[2].value);
            mats[i].SetFloat("_Fill", sliders[3].value);
            mats[i].SetColor("_LineColor", lineColor);
            mats[i].SetColor("_BGColor", Color.cyan);
        }
    }
}