using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    private Player player;
    private List<ICommand> commands;
    private ICommand moveCommand;
    private ICommand mouseCommand;

    public InputHandler(Player _player)
    {
        player = _player;
        commands = new List<ICommand>();
        moveCommand = new MoveCommand();
        mouseCommand = new MouseViewCommand();
    }

    public void HandleInput()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) moveCommand.Execute(player);
        if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0) mouseCommand.Execute(player);
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
                case InputTypeEnum.GetAxis:
                    if (Input.GetAxis(command.Key) != 0) command.Execute(player);
                    break;
                default:
                    Debug.LogError("No InputType found");
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
