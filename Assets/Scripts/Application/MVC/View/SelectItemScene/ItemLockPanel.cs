using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 作为主题选择的子面板
/// </summary>
public class ItemLockPanel : MonoBehaviour
{
    public Button btnSure;
    public Image imgItem0; // 主题0图片
    public Image imgItem1; // 主题1图片

    public bool ShowItem0
    {
        set
        {
            imgItem0.gameObject.SetActive(value);
            imgItem1.gameObject.SetActive(!value);
        }
    }
    
    public bool ShowItem1
    {
        set
        {
            imgItem0.gameObject.SetActive(!value);
            imgItem1.gameObject.SetActive(value);
        }
    }

    private void Start()
    {
        btnSure.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
