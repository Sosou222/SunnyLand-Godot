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

    protected float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override bool Init<T>(T owner)
    {
        if (owner as Player == null)
        {
            return false;
        }
        this.owner = owner as Player;
        return true;
    }

    protected void TryFallFromPlatform()
    {
        if (Input.IsActionJustPressed("PlayerDown"))
        {
            owner.GlobalPosition = owner.GlobalPosition + new Vector2(0, 1);
            owner.MoveAndSlide();
        }
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
