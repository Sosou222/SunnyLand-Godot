using Godot;
using System;

public partial class TextBoxDetector : Area2D
{

	[Export]
	private MarginContainer cointaner;

	[Export]
	private int minumumLenght = 25;
	[Export]
	private int maximumLenght = 50;

	private Player player = null;

	public override void _Process(double delta)
	{
		if (player != null)
		{
			var lenght = (player.GlobalPosition - GlobalPosition).Length();
			Color mod = cointaner.Modulate;

			if (lenght <= minumumLenght)
			{
				cointaner.Visible = true;
				mod.A = 1.0f;
			}
			else if (lenght > maximumLenght)
			{
				cointaner.Visible = false;
				mod.A = 0.0f;
			}
			else
			{
				cointaner.Visible = true;
				mod.A = 1.0f - ((lenght - minumumLenght / minumumLenght) / 100.0f);
			}
			cointaner.Modulate = mod;
		}
	}

	private void OnBodyEnter(Node2D body)
	{
		if (body is Player p)
		{
			GD.Print("Text:Player has entered");
			player = p;
		}
	}

	private void OnBodyExit(Node2D body)
	{
		if (body is Player p)
		{
			GD.Print("Text:Player has exited");
			player = null;
		}
	}
}
