using Godot;
using System;

public partial class Collectible : Area2D
{
	[Export]
	private string collectibleName = "";

	private string pickupAniamtion = "PickUp";

	private AnimationPlayer animationPlayer;

	public override void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	private void OnBodyEnter(Node2D node)
	{
		if (node is Player player)
		{
			GD.Print($"Body entered:{player.Name}");
			player.AddCollectible(collectibleName);

			SetDeferred("monitoring", false); // Can't use 'Monitoring = false;' beacuse it is 'monitoring' currently. Probably would work outside this signal function...mabye

			PickupTween();
			AudioManager.PlayeSound("PickUp");
		}
	}

	private void PickupTween()
	{
		var tween = GetTree().CreateTween();
		tween.TweenProperty(this, "position", Position - new Vector2(0, 50), 0.5);

		tween.TweenCallback(new Callable(this, MethodName.PlayPickupAnimation));
	}

	private void PlayPickupAnimation()
	{
		animationPlayer.Play(pickupAniamtion);
		var tween = animationPlayer.CreateTween();
		tween.TweenProperty(this, "modulate:a", 0, 0.4f);

		tween.TweenCallback(new Callable(this, MethodName.QueueFree));
	}
}
