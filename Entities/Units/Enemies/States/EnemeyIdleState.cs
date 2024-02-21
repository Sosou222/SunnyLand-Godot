using Godot;
using System;

[GlobalClass]
public partial class EnemeyIdleState : EnemyState
{
    public override void Enter()
    {
        GD.Print($"{owner.Name} idle state");
    }

    public override void PhysicsUpdate(double delta)
    {
        Vector2 vel = CalculateGravity(owner.Velocity, delta);

        owner.Velocity = vel;
        owner.MoveAndSlide();
    }
}
