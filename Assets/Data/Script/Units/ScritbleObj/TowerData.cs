using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/DataTower")]
public class TowerData : ScriptableObject
{
    public float Damage;

    public float SpeedAttack;

    public float AttackRange;

    public Bullet Bullet;

    public DataStructure DataStructure;

    public bool DrawRadius;
}
