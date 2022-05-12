using System.Collections;
using UnityEngine;


public interface IDamageble
{
    public float Health { get; protected set; }

    public void TakeDamage(float _Damage)
    {
        Health -= _Damage;
    }
}