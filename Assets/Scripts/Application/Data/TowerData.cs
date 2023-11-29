using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerData : ScriptableObject
{
    public int id;
    public int atk; // 攻击力
    public float atkCd; // 攻击间隔
    public int rotaSpeed; // 旋转速度
    public string prefabsPath; // 预设体路径
    public float attackRange; // 攻击范围
    
    public List<string> bulletsPrefabsPath; // 每级子弹预设体路径
    public List<int> prices; // 每级价格
    public List<int> sellPrices; // 每级卖掉价格
    public Sprite icons; // 创建Icon
    public Sprite greyIcons; // 不够价钱Icon
}
