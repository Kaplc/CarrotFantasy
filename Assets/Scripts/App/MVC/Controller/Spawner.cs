using System.Collections;
using System.Collections.Generic;
using App.DataClass.Game.Level;
using App.DataClass.Game.Object;
using App.Generic.BaseObject;
using App.Generic.Map;
using App.MVC.View.GameScene.Object;
using App.MVC.View.GameScene.Object.Carrot;
using App.MVC.View.GameScene.Object.Map;
using App.Static;
using UnityEngine;
using MapData = App.DataClass.Map.MapData;

namespace App.MVC.Controller
{
    /// <summary>
    /// 对象生成器
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        public bool spawnedComplete; // 完成出怪
        public float lastSpawnTime; // 上一只出怪时间
        public int nowWavesCount; // 当前第几波

        public Carrot carrot; // 萝卜
        public Transform startPoint; // 开始路牌位置
        private LevelData levelData;
        private Coroutine spawnCoroutine;
        public List<Monster> monsters = new List<Monster>(); // 已经出生的怪物
        public List<BaseTower> towers = new List<BaseTower>(); // 已创建的塔
        public List<Obstacle> obstaclesList = new List<Obstacle>(); // 已创建的障碍物

        public Monster collectingFiresTarget; // 集火目标
        public Transform signTrans; // 集火标志

        private Clock clock;
        private MapData mapData;
        private List<Cell> pathList;

        private void Awake()
        {
            spawnedComplete = false;

            // 获取当前关卡数据
            levelData = GameManager.Instance.nowLevelData;
            // 更新面板波数显示
            GameFacade.Instance.SendNotification(NotificationName.UIEvent.GAMEPANEL_UPDATE_WAVESCOUNT, (1, levelData.mapData.waveDataList.Count));

            clock = GetComponent<Clock>();
        }

        public void Init(MapData data)
        {
            // 转换地图数据
            pathList = PointClassToCell.ToCellList(data.pathList);
            
            CreateObstacles();
            CreateCarrot();
            CreateStartBrand();
        }

        /// <summary>
        /// 根据保存的地图数据生成障碍物
        /// </summary>
        private void CreateObstacles()
        {
            MapData nowMapData = GameManager.Instance.nowLevelData.mapData;

            for (int i = 0; i < nowMapData.obstacleList.Count; i++)
            {
                ObstaclePointClass obstaclePointClass = nowMapData.obstacleList[i];

                Cell cell = new Cell(new Point(obstaclePointClass.x, obstaclePointClass.y));
                cell.obstacleName = obstaclePointClass.obstacleType.ToString();
                // 创建实例
                Obstacle obstacle = GameManager.Instance.PoolManager.GetObject($"Object/Obstacle/{cell.obstacleName}").GetComponent<Obstacle>();
                obstacle.transform.SetParent(GameManager.Instance.map.transform);
                obstacle.transform.localScale = Vector3.one;
                obstacle.transform.position = Map.GetCellCenterPos(cell);
                cell.obstacle = obstacle.gameObject;
                obstaclesList.Add(obstacle);
            }
        }

        public void SetCollectingFires(Monster monster)
        {
            for (int i = 0; i < towers.Count; i++)
            {
                towers[i].target = monster;
            }

            // 设置集火标志
            collectingFiresTarget = monster;
            signTrans.gameObject.SetActive(true);
            signTrans.SetParent(monster.signFather.transform);
            signTrans.localPosition = Vector3.zero;
            signTrans.localScale = Vector3.one;
        }

        /// <summary>
        /// 升级塔
        /// </summary>
        public void UpGradeTower(Vector3 cellWorldPos)
        {
            BaseTower tower = Map.GetCell(cellWorldPos).tower as BaseTower;
            if (!tower) return;

            // 最大等级直接返回
            if (tower.level == 2) return;

            // 够钱才升级
            if (GameManager.Instance.money < tower.data.prices[tower.level + 1]) return;
            // 扣钱
            GameFacade.Instance.SendNotification(NotificationName.Game.UPDATE_MONEY, -tower.data.prices[tower.level + 1]);
            // 调用更新方法
            tower.UpGrade();
            // 关闭建造面板
            GameFacade.Instance.SendNotification(NotificationName.UI.HIDE_BUILTPANEL);
        }

        /// <summary>
        /// 出售塔
        /// </summary>
        public void SellTower(Vector3 cellWorldPos)
        {
            Cell cell = Map.GetCell(cellWorldPos);
            BaseTower tower = cell.tower as BaseTower;
            if (!tower) return;

            // 加钱
            GameFacade.Instance.SendNotification(NotificationName.Game.UPDATE_MONEY, +tower.data.sellPrices[tower.level]);
            // 回收对象
            GameManager.Instance.PoolManager.PushObject(tower.gameObject);
            // 清空格子
            cell.tower = null;

            // 关闭建造面板
            GameFacade.Instance.SendNotification(NotificationName.UI.HIDE_BUILTPANEL);
            // 从列表移除
            towers.Remove(tower);
        }

        /// <summary>
        /// 创建塔对象
        /// </summary>
        /// <param name="towerData"></param>
        /// <param name="cellWorldPos">创建的位置世界坐标</param>
        public void CreateTowerObject(TowerData towerData, Vector3 cellWorldPos)
        {
            // 够钱才创建
            if (GameManager.Instance.money >= towerData.prices[0])
            {
                BaseTower tower = GameManager.Instance.PoolManager.GetObject(towerData.prefabsPath).GetComponent<BaseTower>();
                tower.transform.SetParent(GameManager.Instance.map.transform);
                tower.transform.localScale = Vector3.one;
                tower.transform.position = cellWorldPos;
                // 扣钱
                GameFacade.Instance.SendNotification(NotificationName.Game.UPDATE_MONEY, -towerData.prices[0]);
                // 记录该格子已经存在塔
                Map.GetCell(cellWorldPos).tower = tower;

                // 关闭建造面板
                GameFacade.Instance.SendNotification(NotificationName.UI.HIDE_BUILTPANEL);
                // 添加进列表
                if (!towers.Contains(tower))
                {
                    towers.Add(tower);
                }
            }
        }

        /// <summary>
        /// 创建萝卜
        /// </summary>
        private void CreateCarrot()
        {
            carrot = GameManager.Instance.PoolManager.GetObject("Object/Carrot").GetComponent<Carrot>();
            carrot.transform.SetParent(GameManager.Instance.map.transform);
            carrot.transform.localScale = Vector3.one;
            // 设置萝卜位置
            Cell lastPathCell = pathList[pathList.Count - 1];
            carrot.transform.position = Map.GetCellCenterPos(lastPathCell);
        }

        /// <summary>
        /// 创建开始路牌
        /// </summary>
        private void CreateStartBrand()
        {
            startPoint = Instantiate(Resources.Load<GameObject>("Object/StartPoint")).GetComponent<Transform>();
            startPoint.SetParent(GameManager.Instance.map.transform);
            startPoint.localScale = Vector3.one;
            // 设置开始路牌位置
            Cell firstPathCell = pathList[0];
            startPoint.position = Map.GetCellCenterPos(firstPathCell);
        }
        
        public void StopSpawn()
        {
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
            }
        }

        /// <summary>
        /// 回收所有游戏対象
        /// </summary>
        public void OnPushAllGameObject()
        {
            // 销毁标志
            Destroy(signTrans.gameObject);

            OnPushAllTowers();
            OnPushAllMonsters();
            OnPushAllObstacles();
        }

        /// <summary>
        /// 回收未死亡的怪物
        /// </summary>
        private void OnPushAllMonsters()
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                if (!monsters[i].isDead)
                {
                    GameManager.Instance.PoolManager.PushObject(monsters[i].gameObject);
                }
            }

            monsters.Clear();
        }

        private void OnPushAllTowers()
        {
            for (int i = 0; i < towers.Count; i++)
            {
                GameManager.Instance.PoolManager.PushObject(towers[i].gameObject);
            }

            towers.Clear();
        }

        private void OnPushAllObstacles()
        {
            for (int i = 0; i < obstaclesList.Count; i++)
            {
                if (!obstaclesList[i].isDead)
                {
                    GameManager.Instance.PoolManager.PushObject(obstaclesList[i].gameObject);
                }
            }

            obstaclesList.Clear();
        }
    }
}