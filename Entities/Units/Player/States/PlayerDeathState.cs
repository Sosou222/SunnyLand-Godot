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

        tween.TweenProperty(owner.GetNode<AnimatedSprite2D>("AnimatedSprite2D"), "position", new Vector2(0, -40), 1.0f).SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Circ);
        tween.TweenProperty(owner.GetNode<AnimatedSprite2D>("AnimatedSprite2D"), "position", new Vector2(0, 200), 1.0f).SetEase(Tween.EaseType.In).SetTrans(Tween.TransitionType.Back);

        tween.TweenCallback(Callable.From(() => SceneManager.instance.ReloadScene()));

        AudioManager.PlayeSound("PlayerDeath");
    }

    public override void Update(double delta)
    {
        base.Update(delta);
    }
}
