using Godot;
using System;


[GlobalClass]
public partial class EnemyDeathState : EnemyState
{
    [Export]
    private string deathAnimation = "Death";
    public override void Enter()
    {
        owner.animationPlayer.Play(deathAnimation);
        GD.Print($"{owner.Name} death state");

        owner.Velocity = Vector2.Zero;

        Die();
        AudioManager.PlayeSound("EnemyDeath");
    }

    public override void PhysicsUpdate(double delta)
    {
        Vector2 vel = CalculateGravity(owner.Velocity, delta);

        owner.Velocity = vel;
        owner.MoveAndSlide();
    }

    private void Die()
    {
        double lenght = owner.animationPlayer.CurrentAnimationLength;

        var tween = owner.animationPlayer.CreateTween();
        tween.TweenProperty(owner, "modulate:a", 0, 0.4f);

        tween.TweenCallback(new Callable(owner, MethodName.QueueFree));
    }
}
