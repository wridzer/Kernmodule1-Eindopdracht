using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    private GameObject actor;
    private ICommand mouse0Command;
    private ICommand spaceCommand;
    private ICommand moveCommand; 

    public InputHandler(GameObject _actor, ICommand _mouse0, ICommand _spaceCommand)
    {
        actor = _actor;
        mouse0Command = _mouse0;
        spaceCommand = _spaceCommand;
        moveCommand = new MoveCommand();
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) mouse0Command.Execute(actor);
        if (Input.GetKeyDown(KeyCode.Space)) spaceCommand.Execute(actor);
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) moveCommand.Execute(actor) ;
    }
}
