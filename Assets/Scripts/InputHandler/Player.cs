using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public float damage;
    public float speed;
    public float jumpHeight;
    public float gravityValue = -9.81f;

    public Transform viewCamera;
    public float mouseSensitivity = 100f;

    private InputHandler inputHandler;
    public Vector3 playerVelocity;
    public float distToGround = 0.1f;
    
    public Vector3 move;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        distToGround = controller.bounds.extents.y;
        AddInputHandler();
    }

    void Update()
    {
        move = new Vector3();
        inputHandler.HandleInput();
        UpdatePosition();
    }

    private void AddInputHandler()
    {
        inputHandler = new InputHandler(this);
        inputHandler.AddCommand("Horizontal", InputTypeEnum.GetAxis, new HorizontalMoveCommand());
        inputHandler.AddCommand("Vertical", InputTypeEnum.GetAxis, new VerticalMoveCommand());
        inputHandler.AddCommand("Mouse Y", InputTypeEnum.GetAxis, new MouseYViewCommand());
        inputHandler.AddCommand("Mouse X", InputTypeEnum.GetAxis, new MouseXViewCommand());
        inputHandler.AddCommand("space", InputTypeEnum.GetKey, new JumpCommand());
        inputHandler.AddCommand("mouse 0", InputTypeEnum.GetKeyDown, new FireBulletCommand());
    }
    
    void UpdatePosition()
    {
        controller.Move(move * speed * Time.deltaTime);
        
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        if (IsGrounded())
        {
            playerVelocity.y = 0f;
        }
    }
    
    public bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}
