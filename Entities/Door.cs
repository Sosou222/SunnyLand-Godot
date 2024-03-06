using Godot;
using System;

public partial class Door : Area2D
{
	[Export]
	private string nextLevel = "MainMenu";

	private Player player = null;
	public override void _Process(double delta)
	{
		if (player == null)
			return;
		if (Input.IsActionJustPressed("PlayerUp"))
		{
			SceneManager.instance.LoadScene(nextLevel);
		}
	}

	private void OnBodyEnter(Node2D node)
	{
		if (node is Player p)
		{
			player = p;
		}
	}

	private void OnBodyExit(Node2D node)
	{
		if (node is Player p)
		{
			player = null;
		}
	}
}
