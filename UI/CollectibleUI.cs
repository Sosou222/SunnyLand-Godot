using Godot;
using System;

public partial class CollectibleUI : Control
{
	private Label label;
	public override void _Ready()
	{
		label = GetNode<Label>("Label");
	}

	public void UpdateValue(int newValue)
	{
		label.Text = $"x {newValue}";
	}
}
