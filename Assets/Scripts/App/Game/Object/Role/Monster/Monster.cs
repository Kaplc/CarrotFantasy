using System.Collections.Generic;
using App.Data.DataClass.Game.Object;
using App.Game.Generic.BaseObject;
using App.Game.Generic.Map;
using App.UI.GameScene.Control;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace App.Game.Object.Monster
{
    public class Monster : BaseRole, IMonster
    {
        public MonsterData data;

        public float hp;
        public float speed;
        public Animator animator;
        public Transform hpImageBg; // 血条背景图片
        public Transform hpImageFg; // 血条前景图片
        public Transform signFather; // 集火标记父对象

        private readonly List<BaseBuffEffect> buffEffects = new List<BaseBuffEffect>();
        private GameFacade facade;
        private GameManager gameManager;
        private float growth = 1.0f; // 成长系数
        protected float lastWoundTime; // 上次扣血时间

        private Cell nextCell;
        private int pathIndex;

        private List<Cell> pathList;

        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();

            gameManager = GameManager.Instance;
            facade = GameFacade.Instance;
            hpImageBg = transform.Find("HpHolder");
            hpImageFg = transform.Find("HpHolder/HpSlider");
        }

        protected virtual void Start()
        {
            // 默认隐藏血条
            hpImageBg.gameObject.SetActive(false);
        }

        protected virtual void Update()
        {
            Move();

            // 判断是否到达目标格子
            if (Vector3.Distance(Map.Map.GetCellCenterPos(nextCell), transform.position) < 0.1f && IsDead == false)
            {
                // 到达终点格子, 触发死亡方法
                if (pathIndex == pathList.Count - 1)
                {
                    // 终点怪物死亡
                    EndDead();
                    return;
                }

                // 到达换下个目标格子
                pathIndex++;
                pathIndex = Mathf.Clamp(pathIndex, 0, pathList.Count - 1);
                nextCell = pathList[pathIndex];
            }

            // 超过2秒没受到伤害或怪物死亡隐藏血条
            if (Time.time - lastWoundTime > 2 || IsDead) hpImageBg.gameObject.SetActive(false);
        }

        /// <summary>
        ///     点击触发集火
        /// </summary>
        private void OnMouseDown()
        {
            if (IsDead) return;

            // 射线检测判断是否被UI遮挡
            var gr = gameManager.uiManager.canvas.GetComponent<GraphicRaycaster>();
            var eventData = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            var results = new List<RaycastResult>();
            gr.Raycast(eventData, results);
            // 被显示范围的Ui遮挡除外
            if (results.Count > 0 && results[0].gameObject.name != "ImageAttackRange") return;
            // 将自己的位置信息传出
            gameManager.sceneManager.SetFireTarget(this);
        }

        public virtual void Init(List<Cell> list, float hard, MonsterData data)
        {
            this.data = data;
            pathList = list;
            Growth = hard;
            // 位置设置在起点
            transform.position = Map.Map.GetCellCenterPos(pathList[0]);
            // 设置第一个目标格子
            nextCell = pathList[0];
        }

        public void AddBuffEffect(BaseBuffEffect buffEffect)
        {
            buffEffects.Add(buffEffect);
        }

        public override void Wound(int woundHp)
        {
            Hp -= woundHp;
            if (Hp <= 0)
                // 播放死亡音效
                // 播放死亡音效
                gameManager.PlaySound("Music/MonsterDead", 1, false);
        }

        public virtual void SetSpeed(float v)
        {
            speed = v;
        }

        public virtual Transform GetSignFather()
        {
            return signFather;
        }

        public override void Dead()
        {
            // 回收
            gameManager.poolManager.PushObject(gameObject);
            // 怪物死亡触发判断胜利
            gameManager.eventCenter.TriggerEvent("JudgeWin");
            // 记录到统计信息
            gameManager.sceneManager.UpdateKillMonsterCount(+1);
        }

        public override void OnPush()
        {
            // 清空数据
            nextCell = null;
            // 还原动画
            animator.SetBool("Dead", false);
        }

        /// <summary>
        ///     每次从缓存池取出初始化数据
        /// </summary>
        public override void OnGet()
        {
            pathIndex = 0;
            // 刷新属性
            hp = data.maxHp;
            speed = data.speed;
            IsDead = false;
        }

        private void ClearAllBuffs()
        {
            // 移除身上所有Buff
            for (var i = 0; i < buffEffects.Count; i++) gameManager.poolManager.PushObject(buffEffects[i].gameObject);

            gameManager.buffManager.RemoveAllBuffs(this);
        }

        private void Move()
        {
            if (gameManager.sceneManager.IsPause() || IsDead) return;

            var dir = Map.Map.GetCellCenterPos(nextCell) - transform.position;
            dir.Normalize();
            // 移动
            transform.Translate(dir * (Time.deltaTime * speed));
        }

        /// <summary>
        ///     终点死亡
        /// </summary>
        private void EndDead()
        {
            hp = 0;
            IsDead = true;
            // 触发怪物到达终点事件
            gameManager.sceneManager.Spawner.Carrot.Wound((int)data.atk);
            // 移除所有Buff
            ClearAllBuffs();
            // 播放死亡动画
            animator.SetBool("Dead", true);
            // 播放死亡音效
            gameManager.PlaySound("Music/MonsterDead", 1, false);
        }

        #region 属性

        public float Hp
        {
            get => hp;
            set
            {
                hp = value;
                if (hp <= 0)
                {
                    hp = 0;
                    IsDead = true;
                    // 加钱
                    gameManager.sceneManager.UpdateMoney(+(int)(data.baseMoney * growth));
                    // 生成加钱UI
                    var addMoneyTips =
                        gameManager.factoryManager.UIControlFactory.CreateControl("AddMoneyTips").GetComponent<AddMoneyTips>();
                    addMoneyTips.textMeshPro.text = "+" + (int)(data.baseMoney * growth);
                    addMoneyTips.transform.position = transform.position;
                    addMoneyTips.transform.DOMoveY(addMoneyTips.transform.position.y + 2f, 0.5f); // 上移动画
                    // 移除所有Buff
                    ClearAllBuffs();
                    // 如果集火的是自己取消集火标志
                    if ((Monster)gameManager.sceneManager.Spawner.GetCollectingFiresTarget() == this) gameManager.sceneManager.CancelFire();

                    // 播放死亡动画
                    animator.SetBool("Dead", true);
                }

                // 更新血条图片
                hpImageBg.gameObject.SetActive(true);
                hpImageFg.localScale = new Vector3(hp / (data.maxHp * (growth == 0 ? 1 : growth)), 1, 1);
                // 记录显示血条的时间
                lastWoundTime = Time.time;
            }
        }

        public float Growth
        {
            get => growth;
            set
            {
                growth = value;
                // 修改成长系数时自动修改属性值
                hp *= growth;
            }
        }

        public MonsterData Data => data;

        #endregion
    }
}