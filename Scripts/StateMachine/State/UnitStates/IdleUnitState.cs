using Godot;
using System;

public partial class IdleUnitState : AbstractState<Unit>
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
            stateMachine.ChangeState("WalkUnitState");
        }
        if (owner.MovementComponent.WantsToJump())
        {
            stateMachine.ChangeState("JumpUnitState");
        }

    }
}
