using Godot;
using System;

public partial class UnitFallState : AbstractState<Unit>
{
    [Export]
    private string aniamtionFall;

    public override void Enter()
    {
        owner.animationPlayer.Play(aniamtionFall);
    }

    public override void PhysicsUpdate(double delta)
    {
        
    }
}
