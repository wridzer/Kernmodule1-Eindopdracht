using UnityEngine;

public class JumpCommand : ICommand
{
    public InputTypeEnum InputType { get; set; }
    public string Key { get; set; }

    public void Execute(Player _player)
    {
        if (Input.GetKey(KeyCode.Space) && _player.IsGrounded())
        {
            _player.playerVelocity.y = _player.jumpHeight;
        }
    }
}
