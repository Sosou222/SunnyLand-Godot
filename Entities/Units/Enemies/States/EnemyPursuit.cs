using Godot;
using System;

[GlobalClass]
public partial class EnemyPursuit : EnemyState
{
    [Export]
    private string pursitAniamtion = "Pursuit";

    protected static float Speed = 100.0f;

    private const double dirChangeCooldownMax = 0.3f;
    private double dirChangeCooldown = 0.0f;

    private float dirX = 0.0f;
    public override void Enter()
    {
        owner.animationPlayer.Play(pursitAniamtion);
        GD.Print($"{owner.Name} pursuit state");

        dirX = 0.0f;
        dirChangeCooldown = 0.0f;
    }

    public override void PhysicsUpdate(double delta)
    {
        Vector2 vel = CalculateGravity(owner.Velocity, delta);
        dirChangeCooldown -= delta;
        bool isInSight = true;
        if (dirChangeCooldown <= 0)
        {
            dirChangeCooldown = dirChangeCooldownMax;
            if (owner.pdc.IsPlayerInSight())
            {
                Vector2 direction = owner.pdc.GetDirToPlayer();

                if (direction.X < 0.0f)
                {
                    dirX = -1.0f;
                    owner.FlipH(false);
                }
                else
                {
                    dirX = 1.0f;
                    owner.FlipH(true);
                }
            }
            else
            {
                isInSight = false;
            }

        }
        vel.X = Speed * dirX;


        owner.Velocity = vel;
        owner.MoveAndSlide();

        if (!isInSight)
        {
            stateMachine.ChangeState("Idle");
        }
    }

}
