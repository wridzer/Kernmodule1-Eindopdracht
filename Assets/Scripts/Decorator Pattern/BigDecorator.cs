using System.Collections;
using UnityEngine;


public class BigDecorator : BulletDecorator
{
    public BigDecorator(int _damage) : base(_damage) { }

    public override IGun Decorate(IGun _Gun)
    {
        _Gun.BulletTypes |= BulletType.BIG;
        _Gun.GrowAmount++;
        return _Gun;
    }
}