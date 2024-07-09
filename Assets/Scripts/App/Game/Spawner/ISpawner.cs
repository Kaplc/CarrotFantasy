using System.Collections.Generic;
using App.Data.DataClass.Game.Object;
using App.Data.DataClass.Map;
using App.Game.Object.Carrot;
using App.Game.Object.Monster;
using UnityEngine;

namespace App.Game.Spawner
{
    public interface ISpawner
    {
        Carrot Carrot { get; }

        List<IMonster> GetAllMonsters();

        int GetNowWaveCount();

        // 初始化
        void Init(IMapData mapData);

        // 集火
        IMonster GetCollectingFiresTarget();
        void SetCollectingFires(IMonster monster);
        void CancelCollectingFiresTarget();

        #region 缓存池

        void OnPushAllGameObject();

        #endregion

        #region 条件判断

        bool WinJudge();

        #endregion

        #region 出怪

        void StartSpawn();
        void PauseSpawn();
        void ResumeSpawn();

        #endregion

        #region 创建、 升级、出售

        void CreateTowerObject(TowerData towerData, Vector3 cellWorldPos);
        void UpGradeTower(Vector3 cellWorldPos);
        void SellTower(Vector3 cellWorldPos);

        #endregion
    }
}