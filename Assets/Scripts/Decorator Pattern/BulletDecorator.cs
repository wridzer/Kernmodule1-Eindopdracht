using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletDecorator
{
    public int Damage { get; set; }
    public BulletDecorator(int _damage)
    {
        Damage = _damage;
    }

    public abstract IBullet Decorate(IBullet bullet);
}
