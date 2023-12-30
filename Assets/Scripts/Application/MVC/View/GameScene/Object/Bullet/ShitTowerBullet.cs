using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitTowerBullet : BaseBullet
{
    public float effectDuration; // 减速效果持续时间

    protected override void Hit()
    {
        // 障碍物仅受到伤害不会添加Buff
        if (target.CompareTag("Obstacle"))return;
        
        BaseBuff buff = new DecelerationBuff(effectDuration);
        // 怪物添加减速Buff
        GameFacade.Instance.SendNotification(NotificationName.Game.ADD_BUFF, (target, buff));
        // 生成减速特效
        DecelerationEffect effect = target.transform.GetComponentInChildren<DecelerationEffect>();
        if (effect) return;
        effect = GameManager.Instance.PoolManager.GetObject("Object/BuffEffect/DecelerationEffect").GetComponent<DecelerationEffect>();
        effect.transform.SetParent(target.transform, true);
        effect.transform.localPosition = Vector3.zero;
        effect.StartDelayRemove(target, effectDuration, buff); // 开启定时销毁
    }
}