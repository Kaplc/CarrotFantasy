using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecelerationEffect : BaseBuffEffect
{
    private Coroutine coroutine;
    
    /// <summary>
    /// 开启定时回收协程
    /// </summary>
    /// <param name="monster">减速怪物対象</param>
    /// <param name="duration">持续时间</param>
    /// <param name="buff"></param>
    public void StartDelayRemove(Monster monster, float duration, BaseBuff buff)
    {
        coroutine = StartCoroutine(DelayRemoveEffect(monster, duration, buff));
    }

    /// <summary>
    /// 定时回收
    /// </summary>
    /// <param name="monster"></param>
    /// <param name="duration">持续时间</param>
    /// <param name="buff"></param>
    /// <returns></returns>
    private IEnumerator DelayRemoveEffect(Monster monster, float duration, BaseBuff buff)
    {
        while (true)
        {
            yield return new WaitForSeconds(duration);
            if (!GameManager.Instance.Pause)
            {
                break;
            }
        }

        GameFacade.Instance.SendNotification(NotificationName.Game.REMOVE_BUFF, (monster, buff));
        GameManager.Instance.PoolManager.PushObject(gameObject);
    }

    public override void OnGet()
    {
    }

    public override void OnPush()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
}