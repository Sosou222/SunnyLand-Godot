using Godot;
using System;

public partial class WalkUnitState : UnitBaseState
{

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
            return;
        }

        if (!Input.IsActionPressed("PlayerLeft") && !Input.IsActionPressed("PlayerRight"))
        {
            stateMachine.ChangeState("IdleUnitState");
            return;
        }
    }

}
