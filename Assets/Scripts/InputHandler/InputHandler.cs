using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    private Player player;
    private List<ICommand> commands;

    public InputHandler(Player _player)
    {
        player = _player;
        commands = new List<ICommand>();
    }

    public void HandleInput()
    {
        foreach (ICommand command in commands)
        {
            switch (command.InputType)
            {
                case InputTypeEnum.GetKey:
                    if (Input.GetKey(command.Key)) command.Execute(player);
                    break;
                case InputTypeEnum.GetKeyDown:
                    if (Input.GetKeyDown(command.Key)) command.Execute(player);
                    break;
                case InputTypeEnum.GetKeyUp:
                    if (Input.GetKeyUp(command.Key)) command.Execute(player);
                    break;
                case InputTypeEnum.GetAxis:
                    if (Input.GetAxis(command.Key) != 0) command.Execute(player);
                    break;
            }
        }
    }

    public void AddCommand(string _key, InputTypeEnum _inputType, ICommand _command)
    {
        _command.Key = _key;
        _command.InputType = _inputType;
        commands.Add(_command);
    }
}