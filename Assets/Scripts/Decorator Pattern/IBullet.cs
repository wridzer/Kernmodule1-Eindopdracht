using System.Collections;
using UnityEngine;


public interface IBullet
{
    public int Damage { get; set; }
    public int ExplodeRadius { get; set; }
    public int BounceAmount { get; set; }

    public GameObjectPool pool { get; set; }
    public BulletType BulletTypes { get; set; }
    void Hit(GameObject other);

    void Shoot(Vector3 _Dir, float _BulletForce);
}