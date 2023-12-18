using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Image imgLock;
    public Image imgUnlockMapCount;
    public Text txUnlockMapCount;
    
    private bool IsLock
    {
        set
        {
            imgLock.gameObject.SetActive(!value);
            imgUnlockMapCount.gameObject.SetActive(value);
        }
    }

    public void UpdateUnlockMapCount(int unlockCount)
    {
        IsLock = true;
        txUnlockMapCount.text = $"{unlockCount}/9";
    }
}
