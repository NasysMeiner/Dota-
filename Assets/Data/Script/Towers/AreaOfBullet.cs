using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Bullet
{
    [SerializeField] private float areaRadius;
    [SerializeField] private float _damageReductionPercentage = 0.5f;

    protected override void MakeDamage(IEntity enemy)
    {
        base.MakeDamage(enemy);


    }
}