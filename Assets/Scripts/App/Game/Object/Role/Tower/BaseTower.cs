using System;
using System.Collections.Generic;
using System.Linq;
using App.Data.DataClass.Game.Object;
using App.Game.Object.Monster;
using App.Game.Object.Tower;
using App.Game.SceneManager;
using App.Game.Spawner;
using UnityEngine;

namespace App.Game.Generic.BaseObject
{
    [Serializable]
    public abstract class BaseTower : MonoBehaviour, ITower
    {
        public TowerData data;
        public int Atk => data.atkList[level];
        public int level;
        public bool attacking; // 是否正在攻击标识

        public Animator animator;
        public List<RuntimeAnimatorController> controllers;
        public IMonster target; // 当前目标
        private GameObject upGradeTips;
        protected Transform firePos;

        public bool IsDead { get; set; }
        public Transform Transform => transform;

        private ISceneManger SceneManager => GameManager.Instance.sceneManager;
        private bool Pause => SceneManager.IsPause();
        private ISpawner Spawner => SceneManager.Spawner;
        private Dictionary<float, IMonster> targetDic = new Dictionary<float, IMonster>();

        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
        }

        protected virtual void Update()
        {
            if (Pause)
            {
                // 游戏暂停停止炮塔动画
                animator.SetBool("Attack", false);
                attacking = false;
                return;
            }

            // 查找目标
            FindTargets();
            // 开始攻击
            StartAttack();
            // 取消攻击
            CancelAttack();
            // 集火目标
            CollectingFiresTarget();
            // 显示升级提醒
            ShowUpGradeTips();
        }
        
        public void Wound(int woundHp)
        {
            
        }

        private void ShowUpGradeTips()
        {
            // 显示升级提醒
            if (level != 2 && SceneManager.GetMoney() > data.prices[level + 1])
            {
                if (upGradeTips) return;

                upGradeTips = GameManager.Instance.factoryManager.UIControlFactory.CreateControl("UpGradeTips");
                upGradeTips.transform.position = transform.position + new Vector3(0, 0.436f, 0);
            }
            else
            {
                if (upGradeTips)
                {
                    GameManager.Instance.factoryManager.UIControlFactory.PushControl(upGradeTips);
                    upGradeTips = null;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (target != null)
            {
                Gizmos.DrawLine(transform.position, target.Transform.position);
            }
        }

        /// <summary>
        /// 集火目标
        /// </summary>
        private void CollectingFiresTarget()
        {
            // 有集火目标直接锁定
            IMonster monster = Spawner.GetCollectingFiresTarget();
            if (monster != null)
            {
                float distance = Vector3.Distance(transform.position, monster.Transform.position);

                // 处于攻击范围
                if (distance < data.attackRangesList[level] && !monster.IsDead)
                {
                    target = monster;
                }
            }
        }

        /// <summary>
        /// 查找目标
        /// </summary>
        protected void FindTargets()
        {
            if (target != null) return;

            List<IMonster> monsters = Spawner.GetAllMonsters();
            targetDic.Clear();
            // 获取距离list
            foreach (IMonster m in monsters)
            {
                float distance = Vector3.Distance(transform.position, m.Transform.position);
                // 处于攻击范围
                if (distance < data.attackRangesList[level] && !m.IsDead)
                {
                    targetDic[distance] = m;
                }
            }

            // 选择最近目标
            if (targetDic.Count > 0)
            {
                target = targetDic[targetDic.Keys.Min()];
            }
        }

        private void StartAttack()
        {
            // 攻击
            if (target == null && attacking) return;

            animator.SetBool("Attack", true);
            attacking = true;
        }

        private void CancelAttack()
        {
            if (target == null)
            {
                animator.SetBool("Attack", false);
                attacking = false;
                return;
            }

            // 大于攻击距离或打死怪物解除锁定
            if (Vector3.Distance(transform.position, target.Transform.position) > data.attackRangesList[level] || target.IsDead)
            {
                animator.SetBool("Attack", false);
                attacking = false;
                target = null;
            }
        }

        public virtual void Attack()
        {
        }

        /// <summary>
        /// 炮塔升级
        /// </summary>
        public virtual void UpGrade()
        {
            // 停止攻击
            animator.SetBool("Attack", false);
            level++;
            // 切换状态机
            animator.runtimeAnimatorController = controllers[level];
            // 升级动画
            animator.SetTrigger("UpGrade");
        }

        public void SetCollectingFiresTarget(IMonster monster)
        {
            target = monster;
        }

        public TowerData GetData()
        {
            return data;
        }

        public int GetLevel()
        {
            return level;
        }

        public virtual void OnGet()
        {
        }

        public virtual void OnPush()
        {
            // 复原数据
            target = null;
            level = 0;
            animator.runtimeAnimatorController = controllers[0];
            // 有升级标记同时回收
            if (upGradeTips)
            {
                GameManager.Instance.factoryManager.UIControlFactory.PushControl(upGradeTips.gameObject);
                upGradeTips = null;
            }
        }
    }
}