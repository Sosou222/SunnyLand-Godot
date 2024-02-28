using Godot;
using System;

public partial class MovingPlatform : Path2D
{
	[Export(PropertyHint.Range, "0,200,0.1")]
	private double platformMoveTime = 2.0f;

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
		tween.TweenProperty(pathFollow2D, "progress_ratio", 1.0f, platformMoveTime).From(0.0f);
		tween.TweenCallback(Callable.From(() => BeginMovement()));
	}

}
