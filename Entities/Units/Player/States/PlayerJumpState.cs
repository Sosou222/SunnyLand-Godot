using Godot;
using System;

[GlobalClass]
public partial class PlayerJumpState : PlayerMovementState
{
    [Export]
    private string animationJump = "Jump";
    [Export]
    private string animationFall = "Fall";

    public override void Enter()
    {
        GD.Print("Jump Unit Enter");

        if (owner.IsOnFloor() && WantsToJump())
        {
            Jump();
            owner.animationPlayer.Play(animationJump);
            GD.Print("Jumping");
        }
        else
        {
            owner.animationPlayer.Play(animationFall);
            GD.Print("Falling");
        }
    }

    private void Jump()
    {
        Vector2 velocity = owner.Velocity;
        velocity.Y = JumpVelocity;
        owner.Velocity = velocity;
        GD.Print(velocity);
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);

        PlayJumpAnimation(velocity.Y);

        if (owner.IsOnFloor() && (Input.IsActionPressed("PlayerLeft") || Input.IsActionPressed("PlayerRight")))
        {
            stateMachine.ChangeState(StateNames.Walk.ToString());
            return;
        }

        if (owner.IsOnFloor())
        {
            stateMachine.ChangeState(StateNames.Idle.ToString());
            return;
        }
    }

    private void PlayJumpAnimation(float y)
    {

        if (y < 0)
        {
            owner.animationPlayer.Play(animationJump);
        }
        else
        {
            owner.animationPlayer.Play(animationFall);
        }
    }
}
