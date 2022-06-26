using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletDecorator : MonoBehaviour
{
    public int Damage { get; set; }

    public BulletDecorator(int _damage)
    {
        Damage = _damage;
    }

    public abstract IGun Decorate(IGun _Gun);

    protected void OnTriggerEnter(Collider other)
    {
        IGun gun = other.gameObject.GetComponent<Player>()?.gun;

        if (gun != null)
        {
            Decorate(gun);
            Destroy(gameObject); // OBJECT POOL??        
        }

    }
}
