using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 便便塔减速Buff
/// </summary>
public class DecelerationBuff : BaseBuff
{

    public DecelerationBuff(float duration) : base(duration)
    {
        
    }

    protected override void OnApplyBuff(Monster monster)
    {
        // 减速
        monster.speed = monster.data.speed / 2f;
    }

    protected override void OnRemoveBuff(Monster monster)
    {
        // 恢复速度
        monster.speed = monster.data.speed;
    }
}
