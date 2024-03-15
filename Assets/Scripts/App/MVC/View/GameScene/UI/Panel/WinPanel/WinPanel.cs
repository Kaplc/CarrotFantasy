using System;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : BasePanel
{
    public Text txWavesCount;
    public Text txTotalWavesCount;
    public Text txLevel;
    public Button btnSelect;
    public Button btnContinue;
    public Image imgGrade;

    public List<Sprite> gradeSprites;

    protected override void Init()
    {
        btnContinue.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.Game.NEXT_LEVEL);
            UIManager.Instance.Hide<WinPanel>(false);
        });
        btnSelect.onClick.AddListener(() =>
        {
            PanelMediator.SendNotification(NotificationName.Game.EXIT_GAME);
            PanelMediator.SendNotification(NotificationName.LoadScene.LOADSCENE_GAME_TO_SELECTLEVEL);
            UIManager.Instance.Hide<WinPanel>(false);
        });
    }

    public void UpdatePanelData(int wavesCount, int totalWavesCount, int levelID)
    {
        txWavesCount.text = wavesCount / 10 + "  " + wavesCount % 10;
        txTotalWavesCount.text = totalWavesCount / 10 + "" + totalWavesCount % 10;
        txLevel.text = (levelID + 1) / 10 + "" + (levelID + 1) % 10;
    }

    public void UpdateGradeImage(EPassedGrade grade)
    {
        switch (grade)
        {
            case EPassedGrade.Copper:
                imgGrade.sprite = gradeSprites[0];
                break;
            case EPassedGrade.Sliver:
                imgGrade.sprite = gradeSprites[1];
                break;
            case EPassedGrade.Gold:
                imgGrade.sprite = gradeSprites[2];
                break;
        }
    }
}

