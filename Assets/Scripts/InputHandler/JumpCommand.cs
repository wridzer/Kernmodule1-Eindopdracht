using UnityEngine;

public class JumpCommand : ICommand
{
    public void Execute(GameObject _actor)
    {
        Debug.Log("player jump");
    }
}
