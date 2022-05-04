using System.Collections;
using UnityEngine;


public class Bullet : IBullet
{
    public int Damage { get; set; }
    public BulletType BulletTypes { get; set; }

    public Bullet(int _damage)
    {
        Damage = _damage;
    }

    public void Hit()
    {
        Debug.Log("Damage done is: " + Damage + " " + BulletTypes);
    }
}