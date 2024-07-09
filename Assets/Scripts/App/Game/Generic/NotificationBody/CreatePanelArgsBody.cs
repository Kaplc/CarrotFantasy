using System.Collections.Generic;
using App.Data.DataClass.Game.Object;
using App.Static.Enum;
using UnityEngine;

namespace App.Game.Generic.NotificationBody
{
    public class CreatePanelArgsBody
    {
        public Vector3 createPos;
        public EBuiltPanelShowDir showDir;
        public Dictionary<TowerData, Sprite> towersDataDic;
    }
}