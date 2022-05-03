using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    private GameObject actor;
    private ICommand mouse0Command;
    private ICommand spaceCommand;
    private ICommand wCommand;
    private ICommand aCommand;
    private ICommand sCommand;
    private ICommand dCommand;

    public InputHandler(GameObject _actor, ICommand _mouse0, ICommand _spaceCommand)
    {
        actor = _actor;
        mouse0Command = _mouse0;
        spaceCommand = _spaceCommand;
        wCommand = new MoveForwardCommand();
        aCommand = new MoveLeftCommand();
        sCommand = new MoveBackCommand();
        dCommand = new MoveRightCommand();
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) mouse0Command.Execute(actor);
        if (Input.GetKeyDown(KeyCode.Space)) spaceCommand.Execute(actor);
        if (Input.GetKey(KeyCode.W)) wCommand.Execute(actor);
        if (Input.GetKey(KeyCode.A)) aCommand.Execute(actor);
        if (Input.GetKey(KeyCode.S)) sCommand.Execute(actor);
        if (Input.GetKey(KeyCode.D)) dCommand.Execute(actor);
    }
}
