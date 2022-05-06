using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardCommand : ICommand
{
    public void Execute(GameObject _actor)
    {
        Vector3 moveVector = new Vector3(1 * Time.deltaTime, 0, 0);
        
        _actor.transform.Translate(moveVector);
    }
}

public class MoveLeftCommand : ICommand
{
    public void Execute(GameObject _actor)
    {
        Vector3 moveVector = new Vector3(0, 0, 1 * Time.deltaTime);
        
        _actor.transform.Translate(moveVector);
    }
}

public class MoveRightCommand : ICommand
{
    public void Execute(GameObject _actor)
    {
        Vector3 moveVector = new Vector3(0, 0, -1 * Time.deltaTime);
        
        _actor.transform.Translate(moveVector);
    }
}

public class MoveBackCommand : ICommand
{
    public void Execute(GameObject _actor)
    {
        Vector3 moveVector = new Vector3(-1 * Time.deltaTime, 0, 0);
        
        _actor.transform.Translate(moveVector);
    }
}

public class MoveCommand : ICommand
{
    public void Execute(GameObject _actor)
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");
        
        Vector3 move = _actor.transform.right * xMove + _actor.transform.forward * zMove;

        float speed = _actor.GetComponent<Player>().speed;
        
        _actor.GetComponent<CharacterController>().Move(move * speed * Time.deltaTime);
    }
}

public class MoveVerticalCommand : ICommand
{
    public void Execute(GameObject _actor)
    {
        Vector3 moveVector = new Vector3(-1 * Time.deltaTime, 0, 0);
        
        _actor.transform.Translate(moveVector);
    }
}