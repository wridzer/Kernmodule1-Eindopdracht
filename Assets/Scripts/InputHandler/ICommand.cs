using System;
using UnityEngine;

public enum InputTypeEnum {GetKey, GetKeyDown, GetAxis}

public interface ICommand
{
    InputTypeEnum InputType { get; set; }
    string Key { get; set; }
    
    void Execute(Player _player);
}
