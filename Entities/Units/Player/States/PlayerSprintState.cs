using Godot;
using System;

[GlobalClass]
public partial class PlayerSprintState : PlayerMovementState
{
    public override void Enter()
    {
        GD.Print("Sprint Player Enter");
        owner.animationPlayer.Play("Run");

        Speed = SprintSpeed;
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);

        if (WantsToJump() || !owner.IsOnFloor())
        {
            stateMachine.ChangeState(StateNames.Jump.ToString());
            return;
        }

        if (Input.IsActionJustPressed("PlayerCrouch") && owner.IsOnFloor())
        {
            stateMachine.ChangeState(StateNames.Crouch.ToString());
            return;
        }

        if (!Input.IsActionPressed("PlayerLeft") && !Input.IsActionPressed("PlayerRight"))
        {
            stateMachine.ChangeState(StateNames.Idle.ToString());
            return;
        }

        if (!WantToSprint() && owner.IsOnFloor())
        {
            stateMachine.ChangeState(StateNames.Walk.ToString());
            return;
        }
    }
}