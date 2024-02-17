using Godot;
using System;

public partial class PlayerWalkState : PlayerState
{
    public override void Enter()
    {
        GD.Print("Walk Unit Enter");
        owner.animationPlayer.Play("Run");
    }

    public override void PhysicsUpdate(double delta)
    {
        BaseMovement(delta);

        if (owner.MovementComponent.WantsToJump() || !owner.IsOnFloor())
        {
            stateMachine.ChangeState(StateNames.Jump.ToString());
            return;
        }

        if (!Input.IsActionPressed("PlayerLeft") && !Input.IsActionPressed("PlayerRight"))
        {
            stateMachine.ChangeState(StateNames.Idle.ToString());
            return;
        }
    }
}
