using Godot;
using System;

[GlobalClass]
public partial class EnemyEaglePursuitState : EnemyState
{

    private static float Speed = 80.0f;

    private const double dirChangeCooldownMax = 0.1f;
    private double dirChangeCooldown = 0.0f;

    private double attackStateCooldown = 0.0f;
    private const double attackStateCooldownMax = 0.5f;

    private Vector2 dir = Vector2.Zero;

    private float distanceToPlayer = 70.0f;

    public override void Enter()
    {
        GD.Print($"{owner.Name} pursuit state");

        dir = Vector2.Zero;
        dirChangeCooldown = 0.0f;
    }

    public override void PhysicsUpdate(double delta)
    {
        Vector2 vel = owner.Velocity;
        dirChangeCooldown -= delta;
        bool isInSight = true;
        if (dirChangeCooldown <= 0)
        {
            dirChangeCooldown = dirChangeCooldownMax;
            if (owner.pdc.IsPlayerInSight())
            {
                dir = owner.pdc.GetDirToPlayer();

                owner.FlipH(dir.X < 0.0f ? false : true);
            }
            else
            {
                isInSight = false;
            }

        }
        vel = Speed * dir;


        owner.Velocity = vel;
        owner.MoveAndSlide();

        if (attackStateCooldown >= 0.0f)
        {
            attackStateCooldown -= delta;
        }

        if (owner.pdc.GetDistanceToPlayer() != -1 && owner.pdc.GetDistanceToPlayer() <= distanceToPlayer && attackStateCooldown <= 0.0f)
        {
            stateMachine.ChangeState("Attack");
            attackStateCooldown = attackStateCooldownMax;
        }

        if (!isInSight)
        {
            stateMachine.ChangeState("Idle");
        }
    }
}
