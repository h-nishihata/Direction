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

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetFloat("_Frequency", sliders[0 + i * 2].value);
            mats[i].SetFloat("_Fill", sliders[1 + i * 2].value);
            mats[i].SetColor("_LineColor", lineColor);
            //mats[i].SetFloat("_BGColor", shininess);
        }
    }
}