using Godot;
using System;

[GlobalClass]
public abstract partial class PlayerState : BaseState
{
    protected Player owner;

    public enum StateNames
    {
        Idle,
        Walk,
        Jump,
        Crouch,
        Sprint,
        Fall,
        Hurt
    }

    public override bool Init<T>(T owner)
    {
        if (owner as Player == null)
        {
            return false;
        }
        this.owner = owner as Player;
        return true;
    }

    protected Vector2 GetInputVector()
    {
        return Input.GetVector("PlayerLeft", "PlayerRight", "PlayerUp", "PlayerDown"); ;
    }

    protected bool WantsToJump()
    {
        return Input.IsActionJustPressed("PlayerJump");
    }

    protected bool WantToCrouch()
    {
        return Input.IsActionPressed("PlayerCrouch");
    }

    protected bool WantToSprint()
    {
        return Input.IsActionPressed("PlayerSprint");
    }
}
