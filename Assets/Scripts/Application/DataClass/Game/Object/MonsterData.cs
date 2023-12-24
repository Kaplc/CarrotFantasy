using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MonsterData: ScriptableObject
{
    public int id;
    public float speed;
    public int hp;
    public int maxHp;
    public int atk;
    public int baseMoney;
    public string prefabsPath;
}
