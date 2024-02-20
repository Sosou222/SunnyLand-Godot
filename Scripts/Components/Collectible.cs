using Godot;
using System;

public partial class Collectible : Area2D
{
	private void OnBodyEnter(Node2D node)
	{
		if (node is Player player)
		{
			GD.Print($"Body entered:{player.Name}");
		}
	}
}
