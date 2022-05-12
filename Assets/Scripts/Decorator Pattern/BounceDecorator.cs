using System.Collections;
using UnityEngine;


public class BounceDecorator : BulletDecorator
{
    public BounceDecorator(int _damage) : base(_damage) { }

    public override IGun Decorate(IGun _Gun)
    {
        Debug.Log("Splitting");
        _Gun.BulletTypes |= BulletType.BOUNCE;
        _Gun.BounceAmount++;
        return _Gun;
    }
}