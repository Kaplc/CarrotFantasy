using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTower : MonoBehaviour
{
    public int towerID;

    public Button button;
    public Image icon;

    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            GameFacade.Instance.SendNotification(NotificationName.CREATE_TOWER, towerID);
        });
    }
}
