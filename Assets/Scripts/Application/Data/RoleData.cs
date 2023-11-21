using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RoleData: ScriptableObject
{
    public int id;
    public float speed;
    public int hp;
    public string prefabsPath;
}
