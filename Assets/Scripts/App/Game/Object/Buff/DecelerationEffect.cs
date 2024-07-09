using System.Collections;
using App.Game.Generic.BaseObject;
using App.Game.Object.Monster;
using UnityEngine;

namespace App.Game.Object.Buff
{
    public class DecelerationEffect : BaseBuffEffect
    {
        private Coroutine coroutine;

        /// <summary>
        ///     开启定时回收协程
        /// </summary>
        /// <param name="monster">减速怪物対象</param>
        /// <param name="duration">持续时间</param>
        /// <param name="buff"></param>
        public void StartDelayRemove(IMonster monster, float duration, BaseBuff buff)
        {
            coroutine = StartCoroutine(DelayRemoveEffect(monster, duration, buff));
        }

        /// <summary>
        ///     定时回收
        /// </summary>
        /// <param name="monster"></param>
        /// <param name="duration">持续时间</param>
        /// <param name="buff"></param>
        /// <returns></returns>
        private IEnumerator DelayRemoveEffect(IMonster monster, float duration, BaseBuff buff)
        {
            while (true)
            {
                yield return new WaitForSeconds(duration);
                if (!GameManager.Instance.sceneManager.IsPause()) break;
            }

            GameManager.Instance.buffManager.RemoveBuff(monster, buff);
            GameManager.Instance.poolManager.PushObject(gameObject);
        }

        public override void OnGet()
        {
        }

        public override void OnPush()
        {
            if (coroutine != null) StopCoroutine(coroutine);
        }
    }
}