using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLockPanel : MonoBehaviour
{
    public Button btnSure;
    public Button btnClose;

    private void Start()
    {
        btnClose.onClick.AddListener(() => { gameObject.SetActive(false); });
        btnSure.onClick.AddListener(() => { gameObject.SetActive(false); });
    }
}