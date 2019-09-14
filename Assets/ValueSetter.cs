using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ValueSetter : MonoBehaviour
{
    public Material[] mats;
    public Slider[] sliders;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetFloat("_Frequency", sliders[0 + i * 2].value);
            mats[i].SetFloat("_Fill", sliders[1 + i * 2].value);
            //mats[i].SetFloat("_LineColor", shininess);
            //mats[i].SetFloat("_BGColor", shininess);
        }
    }
}
