using Godot;
using System;

public partial class Unit : CharacterBody2D
{
    [Export]
    protected StateMachine stateMachine;

    [Export]
    public AnimationPlayer animationPlayer { get; private set; }

    [Export]
    private AnimatedSprite2D AnimatedSprite2D;

    public override void _Ready()
    {
        stateMachine.Init(this);
    }

    public override void _Process(double delta)
    {
        stateMachine.Update(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        stateMachine.PhysicsUpdate(delta);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        stateMachine.ProcessInput(@event);
    }

    public void FlipH(bool flip)
    {
        AnimatedSprite2D.FlipH = flip;
    }

    public void FlipV(bool flip)
    {
        AnimatedSprite2D.FlipV = flip;
    }
}
