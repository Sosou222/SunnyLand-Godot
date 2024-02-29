using Godot;
using System;

public partial class TextBoxDetector : Area2D
{
	private void OnBodyEnter(Node2D body)
	{
		if (body is Player p)
		{

		}
	}

	private void OnBodyExit(Node2D body)
	{
		if (body is Player p)
		{

		}
	}
}
