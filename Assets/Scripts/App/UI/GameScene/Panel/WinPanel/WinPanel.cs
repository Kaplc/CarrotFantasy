using System.Collections.Generic;
using App.Data.DataClass.Player;
using App.Static;
using GameFramework;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI.GameScene.Panel.WinPanel
{
    public class WinPanel : BasePanel
    {
        public Button btnContinue;
        public Button btnSelect;

        public List<Sprite> gradeSprites;
        public Image imgGrade;
        public Text txLevel;
        public Text txTotalWavesCount;
        public Text txWavesCount;

        protected override void Init()
        {
            btnContinue.onClick.AddListener(() =>
            {
                PanelMediator.SendNotification(NotificationName.UI.NEXT_LEVEL);
                UIManager.Instance.Hide<WinPanel>(false);
            });
            btnSelect.onClick.AddListener(() =>
            {
                PanelMediator.SendNotification(NotificationName.UI.SELECT_LEVEL);
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
}