using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleExtend : MonoBehaviour
{
    public Image imgBackGround;
    private Toggle tg;

    private void Start()
    {
        tg = GetComponent<Toggle>();
        
        tg.onValueChanged.AddListener((isOn)=>
        {
            imgBackGround.enabled = !isOn;
        });
        
        // toggle默认为开启
        imgBackGround.enabled = !tg.isOn;
    }
}
