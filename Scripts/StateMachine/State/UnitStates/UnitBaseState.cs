using Godot;
using System;

public partial class UnitBaseState : BaseState
{

    protected const float Speed = 300.0f;
    protected const float JumpVelocity = -400.0f;

    protected float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    protected Unit owner;
    public override bool Init<T>(T owner)
    {
        if (owner as Unit == null)
        {
            return false;
        }
        this.owner = owner as Unit;
        return true;
    }

    protected Vector2 BaseMovement(double delta)
    {
        Vector2 velocity = owner.Velocity;

        if (!owner.IsOnFloor())
            velocity.Y += gravity * (float)delta;

        Vector2 direction = Input.GetVector("PlayerLeft", "PlayerRight", "PlayerUp", "PlayerDown");
        FlipUnit(direction.X);
        if (direction != Vector2.Zero)
        {
            velocity.X = direction.X * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(owner.Velocity.X, 0, Speed);
        }

        owner.Velocity = velocity;
        owner.MoveAndSlide();

        return velocity;
    }

    protected void FlipUnit(float x)
    {
        if (x > 0)
        {
            owner.FlipH(false);
        }
        if (x < 0)
        {
            owner.FlipH(true);
        }

    }
}
