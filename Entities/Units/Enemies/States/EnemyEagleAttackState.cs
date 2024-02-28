using Godot;
using System;

[GlobalClass]
public partial class EnemyEagleAttackState : EnemyState
{
    private const double attackCooldownMax = 0.4f;
    private double attackCooldown = 0.0f;

    private float Speed = 250.0f;

    private Tween tween;

    Vector2 dir = Vector2.Zero;
    public override void Enter()
    {
        GD.Print($"{owner.Name} attack state");
        owner.animationPlayer.SpeedScale = 3.0f;

        dir = owner.pdc.GetDirToPlayer();
        attackCooldown = attackCooldownMax;
        owner.Velocity = Vector2.Zero;
    }

    public override void PhysicsUpdate(double delta)
    {

        if (attackCooldown <= 0.0f)
        {
            attackCooldown -= delta;
        }
        else
        {
            Charge();
            owner.MoveAndSlide();
        }

    }


    public override void Exit()
    {
        owner.animationPlayer.SpeedScale = 1.0f;
        if (tween != null)
        {
            tween.Kill();
            tween = null;
        }

    }

    private void Charge()
    {
        if (tween != null)
            return;

        Vector2 vel = owner.Velocity;
        vel = Speed * dir;
        owner.Velocity = vel;

        tween = owner.animationPlayer.CreateTween();
        tween.TweenProperty(owner, "velocity", Vector2.Zero, 1.5f).SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Quad);
        tween.TweenCallback(Callable.From(() => stateMachine.ChangeState("Idle")));
    }
}
