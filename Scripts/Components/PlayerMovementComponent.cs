using Godot;
using System;

[GlobalClass]
public partial class PlayerMovementComponent : MovementComponent
{
    public override Vector2 GetMovementDir()
    {
        return Input.GetVector("PlayerLeft","PlayerRight","PlayerUp","PlayerDown").Normalized();
    }

    public override bool WantsToJump()
    {
        return Input.IsActionJustPressed("PlayerJump");
    }
}
