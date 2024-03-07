using Godot;
using System;

[GlobalClass]
public partial class PlayerHurtState : PlayerMovementState
{
    [Export]
    private string animationHurt = "Hurt";

    private static int tim = 0;
    public override void Enter()
    {
        GD.Print("Hurt Player Enter");
        owner.animationPlayer.Play(animationHurt);
        AudioManager.PlayeSound("PlayerHurt");
    }

    public override void PhysicsUpdate(double delta)
    {
        velocity = owner.Velocity;
        if (!owner.IsOnFloor())
            velocity.Y += gravity * (float)delta;

        GD.Print(velocity.X);
        owner.Velocity = velocity;
        owner.MoveAndSlide();
        tim++;
        GD.Print("Tim:" + tim);
    }
}
