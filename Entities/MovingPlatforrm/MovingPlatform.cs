using Godot;
using System;

public partial class MovingPlatform : Path2D
{
	private enum LoopSetting
	{
		None,
		Loop,
		BackToBack
	}


	[Export(PropertyHint.Range, "0,200,0.1")]
	private double platformMoveTime = 2.0f;
	[Export]
	private LoopSetting loppSet = LoopSetting.Loop;
	[Export]
	private Tween.EaseType easeType = Tween.EaseType.In;
	[Export]
	private Tween.TransitionType transitionType = Tween.TransitionType.Linear;

	private PathFollow2D pathFollow2D;

	private Tween tween;
	public override void _Ready()
	{
		pathFollow2D = GetNode<PathFollow2D>("PathFollow2D");
		if (pathFollow2D != null)
		{
			BeginMovement();
		}
		else
		{
			GD.PrintErr($"{Name} has no PathFollow2D");
		}
	}

	private void BeginMovement()
	{
		tween = GetTree().CreateTween();
		tween.TweenProperty(pathFollow2D, "progress_ratio", 1.0f, platformMoveTime).From(0.0f).SetEase(easeType).SetTrans(transitionType);
		if (loppSet == LoopSetting.Loop)
			tween.TweenCallback(Callable.From(() => BeginMovement()));
		if (loppSet == LoopSetting.BackToBack)
			tween.TweenCallback(Callable.From(() => MoveBackwards()));
	}

	private void MoveBackwards()
	{
		tween = GetTree().CreateTween();
		tween.TweenProperty(pathFollow2D, "progress_ratio", 0.0f, platformMoveTime).From(1.0f).SetEase(easeType).SetTrans(transitionType);
		tween.TweenCallback(Callable.From(() => BeginMovement()));
	}

}
