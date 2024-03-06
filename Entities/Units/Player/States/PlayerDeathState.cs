using Godot;
using System;

[GlobalClass]
public partial class PlayerDeathState : PlayerState
{
    [Export]
    private string animationHurt = "Hurt";

    private Tween tween = null;
    public override void Enter()
    {
        GD.Print("Death Player Enter");
        owner.animationPlayer.Play(animationHurt);

        tween = owner.animationPlayer.CreateTween();

        tween.TweenProperty(owner.GetNode<AnimatedSprite2D>("AnimatedSprite2D"), "position", new Vector2(0, -40), 1.0f);
        tween.TweenProperty(owner.GetNode<AnimatedSprite2D>("AnimatedSprite2D"), "position", new Vector2(0, 200), 1.0f);

        tween.TweenCallback(Callable.From(() => SceneManager.instance.ReloadScene()));
    }

    public override void Update(double delta)
    {
        base.Update(delta);
    }
}
