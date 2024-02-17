using Godot;
using System;

public partial class JumpUnitState : AbstractState<Unit>
{
    [Export]
    private string animationJump = "Jump";
    [Export]
    private string animationFall = "Fall";

    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    public const float Speed = 300.0f;
    public const float JumpVelocity = -400.0f;

    public override void Enter()
    {
        GD.Print("Jump Unit Enter");
        owner.animationPlayer.Play(animationJump);
        if(owner.IsOnFloor() && owner.MovementComponent.WantsToJump())
        {
            GD.Print("Jumping");
            Vector2 velocity = owner.Velocity;
            velocity.Y = JumpVelocity;
            owner.Velocity = velocity;
        }
    }

    public override void PhysicsUpdate(double delta)
    {
        Vector2 velocity = BaseMovement(delta);
        GD.Print(velocity);

        PlayJumpAnimation(velocity);

        if (owner.IsOnFloor() && (Input.IsActionPressed("PlayerLeft") || Input.IsActionPressed("PlayerRight")))
        {
            stateMachine.ChangeState("UnitWalkState");
            return;
        }

        if (owner.IsOnFloor())
        {
            stateMachine.ChangeState("UnitIdleState");
            return;
        }
    }

    private void PlayJumpAnimation(Vector2 vel)
    {
        if (vel.Y < 0)
        {
            owner.animationPlayer.Play(animationJump);
        }
        else
        {
            owner.animationPlayer.Play(animationFall);
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
