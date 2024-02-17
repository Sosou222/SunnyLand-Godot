using Godot;
using System;

[GlobalClass]
public partial class PlayerIdleState : PlayerState
{
    public override void Enter()
    {
        GD.Print("Idle Unit Enter");
        owner.animationPlayer.Play("Idle");
        owner.Velocity = new Vector2(0, 0);
    }

    public override void Update(double delta)
    {

    }

    public override void PhysicsUpdate(double delta)
    {
        if (owner.MovementComponent.GetMovementDir().X != 0.0f)
        {
            stateMachine.ChangeState(StateNames.Walk.ToString());
            return;
        }

        if (Input.IsActionJustPressed("PlayerCrouch") && owner.IsOnFloor())
        {
            stateMachine.ChangeState(StateNames.Crouch.ToString());
            return;
        }

        if (owner.MovementComponent.WantsToJump())
        {
            stateMachine.ChangeState(StateNames.Jump.ToString());
            return;
        }

    }
}
