using System.Collections;
using UnityEngine;


public class SplitDecorator : BulletDecorator
{
    public SplitDecorator(int _damage) : base(_damage) { }

    public override IGun Decorate(IGun _Gun)
    {
        Debug.Log("Splitting");
        _Gun.BulletTypes |= BulletType.SPLIT;
        _Gun.BulletAmount++;
        return _Gun;
    }
}