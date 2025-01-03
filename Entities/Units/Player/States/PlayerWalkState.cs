using Godot;
using System;

[GlobalClass]
public partial class PlayerWalkState : PlayerMovementState
{
    public override void Enter()
    {
        GD.Print("Walk Unit Enter");
        owner.animationPlayer.Play("Run");

        Speed = NormalSpeed;
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);

        TryFallFromPlatform();

        if (WantsToJump() && owner.IsOnFloor())
        {
            stateMachine.ChangeState(StateNames.Jump.ToString());
            return;
        }

        if (!owner.IsOnFloor())
        {
            stateMachine.ChangeState(StateNames.Fall.ToString());
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

        if (WantToSprint() && owner.IsOnFloor())
        {
            stateMachine.ChangeState(StateNames.Sprint.ToString());
            return;
        }
    }
}
