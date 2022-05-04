using System.Collections;
using UnityEngine;


public interface IBullet
{
    public int Damage { get; set; }
    public BulletType BulletTypes { get; set; }
    void Hit();
}