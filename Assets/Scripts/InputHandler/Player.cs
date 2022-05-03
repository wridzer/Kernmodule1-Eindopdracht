using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private InputHandler inputHandler;
    public float damage;
    public float jumpHeight;
    
    void Start()
    {
        inputHandler = new InputHandler(gameObject, new FireBulletCommand(), new JumpCommand());
    }

    void Update()
    {
        inputHandler.HandleInput();
    }
}
