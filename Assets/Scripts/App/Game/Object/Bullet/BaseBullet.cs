using App.Data.DataClass.Game.Object;
using App.Game.Object.Monster;
using Library;
using UnityEngine;

namespace App.Game.Generic.BaseObject
{
    public abstract class BaseBullet : MonoBehaviour, IPoolObject
    {
        public int atk;
        public bool active;
    
        public BulletData data;
        public IMonster target;
        protected Transform TargetTsf => ((MonoBehaviour)target).transform;
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Flying();

            if (target != null)
            {
                // 根据距离判断是否击中
                if (Vector3.Distance(transform.position, TargetTsf.position) < 0.3f && active)
                {
                    Hit();
                    // 播放爆炸动画
                    animator.SetTrigger("Explode");
                    // 只扣血一次
                    active = false;
                    // 怪物扣血
                    target.Wound(atk + data.baseAtk);
                }
            }

            // 目标死亡立刻回收
            if (target == null || target.IsDead)
            {
                GameManager.Instance.poolManager.PushObject(gameObject);
            }
        }
    
        /// <summary>
        /// 子弹击中回调
        /// </summary>
        protected virtual void Hit()
        {
        
        }

        protected virtual void Flying()
        {
            if (target != null && !target.IsDead)
            {
                transform.LookAt(TargetTsf);
                if (!GameManager.Instance.sceneManger.IsPause())
                {
                    transform.Translate(transform.forward * (Time.deltaTime * data.speed), Space.World);
                }

                // 目标死亡立刻回收
                if (target.IsDead || !TargetTsf.gameObject.activeSelf)
                {
                    GameManager.Instance.poolManager.PushObject(gameObject);
                }
            }
        }
    
        /// <summary>
        /// 爆炸动画完毕回调
        /// </summary>
        protected virtual void Explode()
        {
            // 回收子弹
            DontDestroyOnLoad(gameObject);
            GameManager.Instance.poolManager.PushObject(gameObject);
        }
    
        public virtual void OnPush()
        {
            target = null;
        }

        public virtual void OnGet()
        {
            // 播放飞行动画
            animator.Play("Flying");
            // 重置
            active = true;
        }
    }
}