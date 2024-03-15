using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MonsterData: ScriptableObject
{
    public int id;
    public float speed;
    public float maxHp;
    public float atk;
    public int baseMoney;
    public string prefabsPath;
}
