using Godot;
using System;

public partial class Player : Unit
{
    private double invisiblityFrames = 0.0f;
    private const double invisiblityFramesMax = 2.0f;

    [Export(PropertyHint.Range, "0.0,1000.0,2.0")]
    private float knockbackForce = 200.0f;

    [Export]
    public HealthComponent healthComponent { get; private set; }

    [Signal]
    public delegate void CherryCountChangeEventHandler(int newValue);
    [Signal]
    public delegate void GemCountChangeEventHandler(int newValue);

    public override void _Ready()
    {
        base._Ready();

        if (GlobalPlayerInfo.MaxHealth > 0)
        {
            healthComponent.SetMaxHealth(GlobalPlayerInfo.MaxHealth);
        }
        CallDeferred("emit_signal", SignalName.CherryCountChange, GlobalPlayerInfo.CherryCount);
        CallDeferred("emit_signal", SignalName.GemCountChange, GlobalPlayerInfo.GemCount);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);


        if (invisiblityFrames >= 0.0f)
        {
            invisiblityFrames -= delta;
        }

        if (Input.IsKeyPressed(Key.U))
        {
            Hurt(false, 20);
        }

    }
    public void AddCollectible(string name, int amount = 1)
    {
        if (name == "Cherry")
        {
            GlobalPlayerInfo.CherryCount += amount;
            EmitSignal(SignalName.CherryCountChange, GlobalPlayerInfo.CherryCount);
            GD.Print($"Cherry count:{GlobalPlayerInfo.CherryCount}");
        }
        if (name == "Gem")
        {
            GlobalPlayerInfo.GemCount += amount;
            EmitSignal(SignalName.GemCountChange, GlobalPlayerInfo.GemCount);
            GD.Print($"Gem count:{GlobalPlayerInfo.GemCount}");
        }
    }

    public void Jump()
    {
        stateMachine.ChangeState(PlayerState.StateNames.Jump.ToString());
    }

    public void Hurt(bool knockbackToLeft, int damage = 1)
    {
        if (invisiblityFrames <= 0.0f)
        {
            healthComponent.TakeDamage(damage);
            if (healthComponent.Health == 0)
            {
                stateMachine.ChangeState(PlayerState.StateNames.Death.ToString());

                return;
            }
            invisiblityFrames = invisiblityFramesMax;

            Vector2 vel = Velocity;
            vel = knockbackForce * new Vector2(knockbackToLeft ? -1.0f : 1.0f, -1f);
            Velocity = vel;

            var tween = animationPlayer.CreateTween();

            stateMachine.ChangeState(PlayerState.StateNames.Hurt.ToString());

            tween.TweenProperty(this, "velocity:x", 0.0f, 0.6f);
            tween.TweenCallback(Callable.From(() => stateMachine.ChangeState("Fall")));

            //Can't really do it in one tween without messing up timing since it seems paralel activates after callback
            //so if I want velocity tween and modulate tween at the same time i can't have callback
            //since it runs with parallel with callback
            var tween2 = animationPlayer.CreateTween();
            tween2.TweenProperty(this, "modulate", Colors.White, invisiblityFramesMax / 5).From(Colors.Red);
            tween2.TweenProperty(this, "modulate", Colors.Red, invisiblityFramesMax / 5);
            tween2.TweenProperty(this, "modulate", Colors.White, invisiblityFramesMax / 5);
            tween2.TweenProperty(this, "modulate", Colors.Red, invisiblityFramesMax / 5);
            tween2.TweenProperty(this, "modulate", Colors.White, invisiblityFramesMax / 5);
        }

    }
}
