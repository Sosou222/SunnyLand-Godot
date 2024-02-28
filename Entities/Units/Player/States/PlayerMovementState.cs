using Godot;
using System;

public abstract partial class PlayerMovementState : PlayerState
{
    protected const float SprintSpeed = 240.0f;
    protected const float NormalSpeed = 180.0f;
    protected static float Speed = 180.0f;
    protected const float JumpVelocity = -320.0f;

    protected Vector2 velocity;

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);
        velocity = owner.Velocity;
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
