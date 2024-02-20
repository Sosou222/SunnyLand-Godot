using Godot;
using System;

public partial class Collectible : Area2D
{
	[Export]
	private string collectibleName = "";

	private void OnBodyEnter(Node2D node)
	{
		if (node is Player player)
		{
			GD.Print($"Body entered:{player.Name}");
			player.AddCollectible(collectibleName);

			SetDeferred("monitoring", false); // Can't use 'Monitoring = false;' beacuse it is 'monitoring' currently. Probably would work outside this signal function...mabye
		}
	}
}
