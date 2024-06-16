using App.Static.Enum;
using UnityEngine;

namespace App.Game.Generic.NotificationBody
{
    public class UpGradeTowerArgsBody
    {
        public Vector3 createPos; // UI创建位置
        public Sprite icon; // 升级图标
        public int upGradeMoney; // 升级价格
        public int sellMoney; // 卖出价格
        public float attackRange; // 攻击范围
        public EBuiltPanelShowDir showDir; // 显示方向
    }
}
