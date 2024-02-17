using Godot;
using System;

public partial class JumpUnitState : UnitBaseState
{
    [Export]
    private string animationJump = "Jump";
    [Export]
    private string animationFall = "Fall";

    public override void Enter()
    {
        GD.Print("Jump Unit Enter");
        owner.animationPlayer.Play(animationJump);
        if (owner.IsOnFloor() && owner.MovementComponent.WantsToJump())
        {
            Jump();
        }
        else
        {
            GD.Print("Falling");
        }
    }

    private void Jump()
    {
        GD.Print("Jumping");
        Vector2 velocity = owner.Velocity;
        velocity.Y = JumpVelocity;
        owner.Velocity = velocity;
    }

    public override void PhysicsUpdate(double delta)
    {
        Vector2 velocity = BaseMovement(delta);

        PlayJumpAnimation(velocity);

        if (owner.IsOnFloor() && (Input.IsActionPressed("PlayerLeft") || Input.IsActionPressed("PlayerRight")))
        {
            stateMachine.ChangeState("WalkUnitState");
            return;
        }

        if (owner.IsOnFloor())
        {
            stateMachine.ChangeState("IdleUnitState");
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
}
