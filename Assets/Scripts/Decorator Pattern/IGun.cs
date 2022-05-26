using System.Collections;
using UnityEngine;

public interface IGun
{
    public GameObject BulletPrefab { get; set; }
    public int Damage { get; set; }
    public float BulletAmount { get; set; }
    public int GrowAmount { get; set; }
    public int ExplodeRadius { get; set; }
    public int BounceAmount { get; set; }
    public BulletType BulletTypes { get; set; }

    public void Shoot();
}