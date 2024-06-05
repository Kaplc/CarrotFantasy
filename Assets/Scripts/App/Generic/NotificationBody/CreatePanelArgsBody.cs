using System.Collections.Generic;
using App.DataClass.Game.Object;
using App.Static.Enum;
using UnityEngine;

namespace App.Generic.NotificationBody
{
    public class CreatePanelArgsBody
    {
        public Vector3 createPos;
        public Dictionary<TowerData, Sprite> towersDataDic;
        public EBuiltPanelShowDir showDir;
    }
}
