using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintCommand : ICommand
{
    public InputTypeEnum InputType { get; set; }
    public string Key { get; set; }

    public void Execute(Player _player)
    {
        _player.speed = 10f;
    }
}

public class SprintReleaseCommand : ICommand
{
    public InputTypeEnum InputType { get; set; }
    public string Key { get; set; }

    public void Execute(Player _player)
    {
        _player.speed = 4f;
    }
}