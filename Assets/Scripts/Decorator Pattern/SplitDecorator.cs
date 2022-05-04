using System.Collections;
using UnityEngine;


public class SplitDecorator : BulletDecorator
{
    public SplitDecorator(int _damage) : base(_damage) { }

    public override IBullet Decorate(IBullet bullet)
    {
        Debug.Log("Splitting");
        bullet.BulletTypes |= BulletType.SPLIT;
        bullet.Damage += Damage;
        return bullet;
    }
}