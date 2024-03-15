using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBuff
{
    public float Duration { get; private set; } // 持续时间
    private Coroutine delayCoroutine;

    public BaseBuff(float duration)
    {
        Duration = duration;
    }

    /// <summary>
    /// 启用Buff
    /// </summary>
    /// <param name="monster"></param>
    public void ApplyBuff(Monster monster)
    {
        OnApplyBuff(monster);

        // 启动计时器，定时移除 Buff
        // 已经在计时重新计时
        if (delayCoroutine != null)
        {
            MonoManager.Instance.StopCoroutine(delayCoroutine);
        }

        delayCoroutine = MonoManager.Instance.StartCoroutine(RemoveBuffAfterDelay(monster));
    }

    /// <summary>
    /// 启用Buff的回调
    /// </summary>
    /// <param name="monster"></param>
    protected virtual void OnApplyBuff(Monster monster)
    {
        // 具体 Buff 逻辑在子类中实现
    }

    private IEnumerator RemoveBuffAfterDelay(Monster monster)
    {
        yield return new WaitForSeconds(Duration);
        RemoveBuff(monster);
    }

    /// <summary>
    /// 移除Buff
    /// </summary>
    /// <param name="monster"></param>
    public void RemoveBuff(Monster monster)
    {
        OnRemoveBuff(monster);
    }

    /// <summary>
    /// 移除Buff的回调
    /// </summary>
    /// <param name="monster"></param>
    protected virtual void OnRemoveBuff(Monster monster)
    {
        // 在子类中实现需要在移除 Buff 时执行的逻辑
    }
}