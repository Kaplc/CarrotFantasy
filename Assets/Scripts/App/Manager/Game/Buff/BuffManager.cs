using System.Collections.Generic;
using App.Generic.BaseObject;
using App.MVC.View.GameScene.Object;
using Library;

namespace App.MVC.Controller
{
    public class BuffManager : BaseSingleton<BuffManager>
    {
        private Dictionary<IMonster, List<BaseBuff>> buffDictionary = new Dictionary<IMonster, List<BaseBuff>>();

        /// <summary>
        /// 怪物添加Buff
        /// </summary>
        /// <param name="monster"></param>
        /// <param name="buff"></param>
        public void ApplyBuff(IMonster monster, BaseBuff buff)
        {
            if (!buffDictionary.ContainsKey(monster))
            {
                buffDictionary[monster] = new List<BaseBuff>();
            }

            // Buff添加进怪物的BuffList
            buffDictionary[monster].Add(buff);
            // 激活Buff
            buff.ApplyBuff(monster);
        }

        /// <summary>
        /// 移除单个Buff
        /// </summary>
        /// <param name="monster"></param>
        /// <param name="buff"></param>
        public void RemoveBuff(IMonster monster, BaseBuff buff)
        {
            if (!buffDictionary.ContainsKey(monster)) return;

            if (buffDictionary[monster].Contains(buff))
            {
                buffDictionary[monster].Remove(buff);
                // 执行Buff移除
                buff.RemoveBuff(monster);
            }
        }

        /// <summary>
        /// 移除怪物上所有Buff
        /// </summary>
        /// <param name="monster"></param>
        public void RemoveAllBuffs(IMonster monster)
        {
            if (!buffDictionary.ContainsKey(monster)) return;

            for (int i = 0; i < buffDictionary[monster].Count; i++)
            {
                buffDictionary[monster][i].RemoveBuff(monster);
            }

            buffDictionary[monster].Clear();
        }

        /// <summary>
        /// 清空所有怪物所有Buff
        /// </summary>
        public void ClearAllBuffs()
        {
            foreach (var item in buffDictionary)
            {
                RemoveAllBuffs(item.Key);
            }
        }
    }
}