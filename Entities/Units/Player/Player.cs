using Godot;
using System;

public partial class Player : Unit
{
    private int cherryCount = 0;
    private int gemCount = 0;

    private double invisiblityFrames = 0.0f;
    private const double invisiblityFramesMax = 2.0f;

    [Export]
    public HealthComponent healthComponent { get; private set; }

    [Signal]
    public delegate void CherryCountChangeEventHandler(int newValue);
    [Signal]
    public delegate void GemCountChangeEventHandler(int newValue);


    public override void _Process(double delta)
    {
        base._Process(delta);

        if (invisiblityFrames >= 0.0f)
        {
            invisiblityFrames -= delta;
        }

    }
    public void AddCollectible(string name, int amount = 1)
    {
        if (name == "Cherry")
        {
            cherryCount += amount;
            EmitSignal(SignalName.CherryCountChange, cherryCount);
            GD.Print($"Cherry count:{cherryCount}");
        }
        if (name == "Gem")
        {
            gemCount += amount;
            EmitSignal(SignalName.GemCountChange, gemCount);
            GD.Print($"Gem count:{gemCount}");
        }
    }

    public void Jump()
    {
        stateMachine.ChangeState(PlayerState.StateNames.Jump.ToString());
    }

    public void Hurt(int damage = 1)
    {
        if (invisiblityFrames <= 0.0f)
        {
            healthComponent.TakeDamage(damage);
            invisiblityFrames = invisiblityFramesMax;
            var tween = animationPlayer.CreateTween();

            tween.TweenProperty(this, "modulate", Colors.White, invisiblityFramesMax / 5).From(Colors.Red);
            tween.TweenProperty(this, "modulate", Colors.Red, invisiblityFramesMax / 5);
            tween.TweenProperty(this, "modulate", Colors.White, invisiblityFramesMax / 5);
            tween.TweenProperty(this, "modulate", Colors.Red, invisiblityFramesMax / 5);
            tween.TweenProperty(this, "modulate", Colors.White, invisiblityFramesMax / 5);
        }

    }
}
