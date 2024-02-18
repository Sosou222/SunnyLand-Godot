using Godot;
using System;

public abstract partial class PlayerMovementState : PlayerState
{
    protected const float Speed = 180.0f;
    protected const float JumpVelocity = -320.0f;

    protected float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    protected Vector2 velocity;

    public override void PhysicsUpdate(double delta)
    {
        Vector2 velocity = owner.Velocity;

        if (!owner.IsOnFloor())
            velocity.Y += gravity * (float)delta;

        Vector2 direction = GetInputVector();
        FlipPlayer(direction.X);
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
    }

    protected void FlipPlayer(float x)
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
