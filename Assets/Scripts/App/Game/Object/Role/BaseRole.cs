using UnityEngine;

namespace App.Game.Generic.BaseObject
{
    public abstract class BaseRole : MonoBehaviour, IRole
    {
        public bool IsDead { get; set; }

        public Transform Transform { get=>transform; }
        
        public virtual void Wound(int woundHp){}

        public virtual void Dead(){}
    
        /// <summary>
        /// 适用于对象池回收时复原
        /// </summary>
        /// <param name="obj"></param>
        public virtual void OnPush(){}
    
        /// <summary>
        /// 使用对象池时初始化
        /// </summary>
        /// <returns></returns>
        public virtual void OnGet(){}
    }
}
