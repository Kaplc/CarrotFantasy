using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour
{
    public int levelID;
    public EPassedGrade passedGrade;
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
                // 根据通关等级选择图片
                imgGarde.gameObject.SetActive(true);
                switch (passedGrade)
                {
                    case EPassedGrade.None:
                        // 未通关的已解锁
                        imgGarde.gameObject.SetActive(false);
                        break;
                    case EPassedGrade.Copper:
                        imgGarde.sprite = gardeSprites[0];
                        break;
                    case EPassedGrade.Sliver:
                        imgGarde.sprite = gardeSprites[1];
                        break;
                    case EPassedGrade.Gold:
                        imgGarde.sprite = gardeSprites[2];
                        break;
                }
            }
        }
    }
}