using UnityEngine;

public class JumpCommand : ICommand
{
    public void Execute(Player _player)
    {
        Debug.Log("player jump");
    }
}
