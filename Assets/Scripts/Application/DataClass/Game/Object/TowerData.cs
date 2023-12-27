using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu][Serializable]
public class TowerData : ScriptableObject
{
    public int id;
    public int rotaSpeed; // 旋转速度
    public string prefabsPath; // 预设体路径
    public List<float> attackRangesList; // 每级攻击范围
    public List<int> atkList; // 每级攻击力
    public List<string> bulletsPrefabsPath; // 每级子弹预设体路径
    public List<int> prices; // 每级价格
    public List<int> sellPrices; // 每级卖掉价格
    public Sprite icon; // 创建Icon
    public Sprite greyIcon; // 不够价钱Icon
    public Sprite selectLevelIcon; // 选择关卡时的icon
}