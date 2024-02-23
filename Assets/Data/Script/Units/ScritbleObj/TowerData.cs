using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/DataTower")]
public class TowerData : ScriptableObject
{
    public float Damage;

    public float SpeedAttack;

    public float AttackTange;

    public Bullet Bullet;
}
