using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public float damage;
    public float speed = 3f;

    public Transform viewCamera;
    public float mouseSensitivity = 100f;

    private InputHandler inputHandler;
    private Vector3 velocity;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        AddInputHandler();
    }

    void Update()
    {
        inputHandler.HandleInput();
    }

    private void AddInputHandler()
    {
        inputHandler = new InputHandler(this);
        inputHandler.AddCommand("mouse 0", InputTypeEnum.GetKeyDown, new FireBulletCommand());
        inputHandler.AddCommand("space", InputTypeEnum.GetKeyDown, new JumpCommand());
    }
}
