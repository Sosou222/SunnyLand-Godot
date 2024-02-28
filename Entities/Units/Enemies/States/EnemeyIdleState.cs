using Godot;
using System;

[GlobalClass]
public partial class EnemeyIdleState : EnemyState
{
    [Export]
    private string idleAnimation = "Idle";
    [Export]
    private bool applyGravity = true;
    public override void Enter()
    {
        owner.animationPlayer.Play(idleAnimation);
        GD.Print($"{owner.Name} idle state");

        owner.Velocity = Vector2.Zero;
    }

    public override void PhysicsUpdate(double delta)
    {
        if (applyGravity)
        {
            Vector2 vel = CalculateGravity(owner.Velocity, delta);

            owner.Velocity = vel;
            owner.MoveAndSlide();
        }

        if (owner.pdc.IsPlayerInSight())
        {
            stateMachine.ChangeState("Pursuit");
        }
    }
}
