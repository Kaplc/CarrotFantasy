using System.Collections.Generic;
using App.DataClass.Game.Object;
using App.DataClass.Map;
using App.MVC.View.GameScene.Object;
using App.MVC.View.GameScene.Object.Carrot;
using UnityEngine;


public interface ISpawner
{
    #region 获取字段

    Carrot GetCarrot();

    List<IMonster> GetAllMonsters();
    int GetNowWaveCount();

    #endregion

    // 初始化
    void Init(MapData mapData);

    // 集火
    IMonster GetCollectingFiresTarget();
    void SetCollectingFires(IMonster monster);
    void CancelCollectingFiresTarget();

    #region 出怪

    void StartSpawn();
    void PauseSpawn();
    void ResumeWaves();

    #endregion

    #region 创建、 升级、出售

    void CreateTowerObject(TowerData towerData, Vector3 cellWorldPos);
    void UpGradeTower(Vector3 cellWorldPos);
    void SellTower(Vector3 cellWorldPos);

    #endregion

    #region 缓存池

    void OnPushAllGameObject();

    #endregion

    #region 条件判断

    bool WinJudge();

    #endregion
}