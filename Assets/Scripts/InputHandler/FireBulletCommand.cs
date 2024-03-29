using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBulletCommand : ICommand
{
    public InputTypeEnum InputType { get; set; }
    public string Key { get; set; }

    public void Execute(Player _player)
    {
        _player.gun.Shoot();
    }
}
