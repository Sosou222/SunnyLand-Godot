using Godot;
using System;

[GlobalClass]
public partial class EnemyPursuit : EnemyState
{
    [Export]
    private string pursitAniamtion = "Pursuit";

    protected static float Speed = 100.0f;
    public override void Enter()
    {
        owner.animationPlayer.Play(pursitAniamtion);
        GD.Print($"{owner.Name} pursuit state");
    }

    public override void PhysicsUpdate(double delta)
    {
        Vector2 vel = CalculateGravity(owner.Velocity, delta);

        bool isInSight = false;
        if (owner.IsPlayerInSight())
        {
            Vector2 dir = owner.GetDirToPlayer();
            float dirx = 1.0f;
            if (dir.X < 0.0f)
            {
                dirx = -1.0f;
                owner.FlipH(false);
            }
            else
            {
                owner.FlipH(true);
            }
            vel.X = Speed * dirx;
            isInSight = true;
        }

        owner.Velocity = vel;
        owner.MoveAndSlide();

        if (!isInSight)
        {
            stateMachine.ChangeState("Idle");
        }
    }

}
