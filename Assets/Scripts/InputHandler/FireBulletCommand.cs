using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBulletCommand : ICommand
{
    public void Execute(Player _player)
    {
        Debug.Log("Fire bullet with damage: " + _player.damage);
    }
}
