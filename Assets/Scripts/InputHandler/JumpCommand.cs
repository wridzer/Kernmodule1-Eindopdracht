using UnityEngine;

public class JumpCommand : ICommand
{
    public InputTypeEnum InputType { get; set; }
    public string Key { get; set; }

    public void Execute(Player _player)
    {
        Debug.Log("player jump");
    }
}
