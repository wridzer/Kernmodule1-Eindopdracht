using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBulletCommand : ICommand
{
    public void Execute(GameObject _actor)
    {
        Debug.Log("Fire bullet with damage: " + _actor.GetComponent<Player>().damage);
    }
}
