using Godot;
using System;

using Godot.Collections;

public partial class Shop : Area2D
{
	[Export]
	private int purcvhasePrice = 5;
	[Export]
	private int purchaseTimes = 1;
	[Export]
	private Array<Node> toDisable = new Array<Node>();
	[Export]
	private Label labelText;
	private Player player = null;

	public override void _Ready()
	{
		labelText.Text = $"      x {purcvhasePrice}\n    =\n\n\n";

	}
	public override void _Process(double delta)
	{
		if (player == null || purchaseTimes == 0)
			return;
		if (Input.IsActionJustPressed("PlayerUp"))
		{
			GD.Print("Shop trying purchase");
			Purchase();
		}
	}
	private void OnBodyEnter(Node2D node)
	{
		if (node is Player p)
		{
			GD.Print("Shop player entered");
			player = p;
		}
	}

	private void OnBodyExit(Node2D node)
	{
		if (node is Player p)
		{
			GD.Print("Shop player entered");
			player = null;
		}
	}

	private void Purchase()
	{
		if (player.gemCount >= purcvhasePrice)
		{
			player.AddCollectible("Gem", -purcvhasePrice);
			purchaseTimes--;
			player.healthComponent.AddMaxHealth();

			if (purchaseTimes == 0)
			{
				foreach (Node node in toDisable)
				{
					node.QueueFree();
				}
			}
		}
	}
}
