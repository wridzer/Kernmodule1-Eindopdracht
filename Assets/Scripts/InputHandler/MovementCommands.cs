using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    public void Execute(Player _player)
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");

        Vector3 move = _player.transform.right * xMove + _player.transform.forward * zMove;
        
        _player.controller.Move(move * _player.speed * Time.deltaTime);
    }
}

public class MouseViewCommand : ICommand
{
    private float angleX = 0;
    
    public void Execute(Player _player)
    {
        float mouseX = Input.GetAxis("Mouse X") * _player.mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _player.mouseSensitivity * Time.deltaTime;

        angleX -= mouseY;
        angleX = Mathf.Clamp(angleX, -90f, 90f);

        _player.camera.transform.localRotation = Quaternion.Euler(angleX, 0f, 0f);
        _player.transform.Rotate(Vector3.up * mouseX);
    }
}