using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMoveCommand : ICommand
{
    public InputTypeEnum InputType { get; set; }
    public string Key { get; set; }

    public void Execute(Player _player)
    {
        float xMove = Input.GetAxisRaw("Horizontal");

        _player.move += _player.transform.right * xMove;
    }
}

public class VerticalMoveCommand : ICommand
{
    public InputTypeEnum InputType { get; set; }
    public string Key { get; set; }

    public void Execute(Player _player)
    {
        float zMove = Input.GetAxisRaw("Vertical");

        _player.move += _player.transform.forward * zMove;
    }
}

public class MouseXViewCommand : ICommand
{
    public InputTypeEnum InputType { get; set; }
    public string Key { get; set; }
    
    public void Execute(Player _player)
    {
        float mouseX = Input.GetAxis("Mouse X") * _player.mouseSensitivity * Time.deltaTime;

        _player.transform.Rotate(Vector3.up * mouseX);
    }
}

public class MouseYViewCommand : ICommand
{
    public InputTypeEnum InputType { get; set; }
    public string Key { get; set; }
    private float angleX = 0;
    
    public void Execute(Player _player)
    {
        float mouseY = Input.GetAxis("Mouse Y") * _player.mouseSensitivity * Time.deltaTime;

        angleX -= mouseY;
        angleX = Mathf.Clamp(angleX, -90f, 90f);

        _player.viewCamera.transform.localRotation = Quaternion.Euler(angleX, 0f, 0f);
    }
}