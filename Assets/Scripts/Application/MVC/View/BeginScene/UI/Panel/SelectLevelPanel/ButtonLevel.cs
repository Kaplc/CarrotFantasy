using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour
{
    public int levelID;
    public int passedGrade;
    private bool isLock = true; // 默认锁定
    public Image imgLock;
    public Image imgMap;
    public Image imgGarde;
    public List<Sprite> gardeSprites; // 通关等级图片

    public bool IsLock
    {
        get => isLock;
        set
        {
            if (value)
            {
                // 显示锁定图片
                isLock = true;
                imgLock.gameObject.SetActive(true);
                imgGarde.gameObject.SetActive(false);
            }
            else // 显示通关等级
            {
                isLock = false;
                imgLock.gameObject.SetActive(false);
                // 未通关的已解锁的为-1
                if (passedGrade == -1) return;
                // 根据通关等级选择图片
                imgGarde.gameObject.SetActive(true);
                imgGarde.sprite = gardeSprites[passedGrade];
            }
        }
    }
}