using Godot;
using System;

[GlobalClass]
public partial class PlayerFallState : PlayerMovementState
{
    [Export]
    private string animationFall = "Fall";
    public override void Enter()
    {
        GD.Print("Fall Unit Enter");
        owner.animationPlayer.Play(animationFall);

    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);

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
}
