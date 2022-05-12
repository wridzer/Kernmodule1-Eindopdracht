using System.Collections;
using UnityEngine;


public class ExplodeDecorator : BulletDecorator
{
    public ExplodeDecorator(int _damage) : base(_damage) { }

    public override IGun Decorate(IGun _Gun)
    {
        Debug.Log("Splitting");
        _Gun.BulletTypes |= BulletType.EXPLODE;
        _Gun.ExplodeRadius++;
        return _Gun;
    }
}