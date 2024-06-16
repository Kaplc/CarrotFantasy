using App.Game.Generic.BaseObject;
using App.Game.Object.Buff;
using App.Static;
using UnityEngine;

namespace App.Game.Object.Bullet
{
    public class ShitTowerBullet : BaseBullet
    {
        public float effectDuration; // 减速效果持续时间

        protected override void Hit()
        {
            // 障碍物仅受到伤害不会添加Buff
            if (TargetTsf.CompareTag("Obstacle"))return;
        
            BaseBuff buff = new DecelerationBuff(effectDuration);
            // 怪物添加减速Buff
            GameManager.Instance.buffManager.ApplyBuff(target,  buff);
            // 生成减速特效
            DecelerationEffect effect = TargetTsf.GetComponentInChildren<DecelerationEffect>();
            if (effect) return;
            effect = GameManager.Instance.poolManager.GetObject("Object/BuffEffect/DecelerationEffect").GetComponent<DecelerationEffect>();
            effect.transform.SetParent(TargetTsf, true);
            effect.transform.localPosition = Vector3.zero;
            effect.StartDelayRemove(target, effectDuration, buff); // 开启定时销毁
        }
    }
}