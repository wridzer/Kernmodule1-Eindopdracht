using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public float damage;
    public float speed = 3f;
    
    private InputHandler inputHandler;
    private Vector3 velocity;


    void Start()
    {
        inputHandler = new InputHandler(gameObject, new FireBulletCommand(), new JumpCommand());
    }

    void Update()
    {
        //inputHandler.HandleInput();
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");
        
        Vector3 move = transform.right * xMove + transform.forward * zMove;
        
        controller.Move(move * speed * Time.deltaTime);
    }
}
