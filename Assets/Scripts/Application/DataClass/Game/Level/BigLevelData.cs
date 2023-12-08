using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BigLevelData : ScriptableObject
{
    public int id;
    public List<LevelData> levels; // 大关卡下的所有小关卡id
}
