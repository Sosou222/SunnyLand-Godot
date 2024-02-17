using Godot;
using System;

[GlobalClass]
public partial class MovementComponent : Node
{
    [Export]
    private CharacterBody2D owner;

    public virtual Vector2 GetMovementDir()
    {
        return Vector2.Zero;
    }

    public virtual bool WantsToJump()
    {
        return false;
    }
}
