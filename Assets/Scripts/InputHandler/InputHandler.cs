using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject actor;
    public Dictionary<string, ICommand> commands;

    public void HandleInput()
    {
        foreach (KeyValuePair<string, ICommand> entry in commands)
        {
            if (Input.GetKey(entry.Key))
            {
                entry.Value.Execute(actor);
            }
        }
    }
}
