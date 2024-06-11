using System;
using System.Collections.Generic;
using App.DataClass.Game.Object;
using App.MVC.Controller;
using App.MVC.View.GameScene.Object;
using App.MVC.View.GameScene.Object.Tower;
using UnityEngine;

namespace App.Generic.BaseObject
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

        public Transform TargetTsf => ((Monster)target)?.transform;

        private GameObject upGradeTips;
        protected Transform firePos;

        private bool Pause => GameManager.Instance.Pause;
        private ISpawner Spawner => GameManager.Instance.spawner;

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

        private void ShowUpGradeTips()
        {
            // 显示升级提醒
            if (level != 2 && GameManager.Instance.money > data.prices[level + 1])
            {
                if (upGradeTips) return;

                upGradeTips = GameManager.Instance.FactoryManager.UIControlFactory.CreateControl("UpGradeTips");
                upGradeTips.transform.position = transform.position + new Vector3(0, 0.436f, 0);
            }
            else
            {
                if (upGradeTips)
                {
                    GameManager.Instance.FactoryManager.UIControlFactory.PushControl(upGradeTips);
                    upGradeTips = null;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (target != null)
            {
                Gizmos.DrawLine(transform.position, TargetTsf.position);
            }
        }

        /// <summary>
        /// 集火目标
        /// </summary>
        private void CollectingFiresTarget()
        {
            // 有集火目标直接锁定
            IMonster monster = GameManager.Instance.spawner.GetCollectingFiresTarget();
            if (monster != null && target != monster)
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
            // 查找目标
            foreach (IMonster m in monsters)
            {
                float distance = Vector3.Distance(transform.position, m.Transform.position);

                // 处于攻击范围
                if (distance < data.attackRangesList[level] && !m.IsDead)
                {
                    target = m;
                    return;
                }
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
            if (Vector3.Distance(transform.position, TargetTsf.position) > data.attackRangesList[level] || target.IsDead)
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
                GameManager.Instance.FactoryManager.UIControlFactory.PushControl(upGradeTips.gameObject);
                upGradeTips = null;
            }
        }
    }
}