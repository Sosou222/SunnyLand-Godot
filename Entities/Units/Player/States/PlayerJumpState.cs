using Godot;
using System;

[GlobalClass]
public partial class PlayerJumpState : PlayerMovementState
{
    [Export]
    private string animationJump = "Jump";

    public override void Enter()
    {
        GD.Print("Jump Unit Enter");
        owner.animationPlayer.Play(animationJump);
        Jump();
        AudioManager.PlayeSound("PlayerJump");
    }

    private void Jump()
    {
        Vector2 velocity = owner.Velocity;
        velocity.Y = JumpVelocity;
        owner.Velocity = velocity;
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);

        if (velocity.Y > 0.0f)
        {
            stateMachine.ChangeState(StateNames.Fall.ToString());
            return;
        }
    }
}
