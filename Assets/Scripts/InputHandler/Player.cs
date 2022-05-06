using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public float damage;
    public float speed = 3f;

    public Transform camera;
    public float mouseSensitivity = 100f;

    private InputHandler inputHandler;
    private Vector3 velocity;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        inputHandler = new InputHandler(this, new FireBulletCommand(), new JumpCommand());
    }

    void Update()
    {
        inputHandler.HandleInput();
    }
}
