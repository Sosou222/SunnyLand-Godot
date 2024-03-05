using Godot;
using System;

public partial class BackgroundA : ParallaxBackground
{
	[Export]
	private bool isAnamtion = false;

	private double scrollSpeed = 100.0f;
	public override void _Process(double delta)
	{
		if (!isAnamtion)
			return;

		ScrollOffset -= new Vector2((float)(scrollSpeed * delta), 0);
	}
}
