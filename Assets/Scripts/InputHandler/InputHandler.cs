using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    private Player player;
    private ICommand mouse0Command;
    private ICommand spaceCommand;
    private ICommand moveCommand; 
    private ICommand mouseViewCommand; 

    public InputHandler(Player _player, ICommand _mouse0, ICommand _spaceCommand)
    {
        player = _player;
        mouse0Command = _mouse0;
        spaceCommand = _spaceCommand;
        moveCommand = new MoveCommand();
        mouseViewCommand = new MouseViewCommand();
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) mouse0Command.Execute(player);
        if (Input.GetKeyDown(KeyCode.Space)) spaceCommand.Execute(player);
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) moveCommand.Execute(player);
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) mouseViewCommand.Execute(player);
    }
}
