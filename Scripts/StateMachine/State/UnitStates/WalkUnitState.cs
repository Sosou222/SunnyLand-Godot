using Godot;
using System;

public partial class WalkUnitState : AbstractState<Unit>
{

    public const float Speed = 300.0f;
    public const float JumpVelocity = -400.0f;


    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void Enter()
    {
        GD.Print("Walk Unit Enter");
        owner.animationPlayer.Play("Run");
    }

    public override void PhysicsUpdate(double delta)
    {
        BaseMovement(delta);

        if (Input.IsActionJustPressed("PlayerJump") || !owner.IsOnFloor())
        {
            stateMachine.ChangeState("JumpUnitState");
        }

        if (!Input.IsActionPressed("PlayerLeft") && !Input.IsActionPressed("PlayerRight"))
        {
            stateMachine.ChangeState("IdleUnitState");
        }
    }

    private Vector2 BaseMovement(double delta)
    {
        Vector2 velocity = owner.Velocity;

        if (!owner.IsOnFloor())
            velocity.Y += gravity * (float)delta;

        Vector2 direction = Input.GetVector("PlayerLeft", "PlayerRight", "PlayerUp", "PlayerDown");
        //FlipPlayer(direction.X);
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
}
