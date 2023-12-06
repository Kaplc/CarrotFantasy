using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EBulletType
{
    Normal,
    Range
}

[CreateAssetMenu]
public class BulletData : ScriptableObject
{
    public EBulletType type; // 子弹类型
    public float speed; // 子弹速度
    public int baseAtk; // 基础攻击力
}
