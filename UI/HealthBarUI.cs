using Godot;
using System;

public partial class HealthBarUI : Control
{
	[Export]
	TextureRect fullHeart;
	[Export]
	TextureRect halfHeart;
	[Export]
	TextureRect emptyHeart;

	[Export]
	Player player;

	private int heartCount = 0;

	public override void _Ready()
	{
		heartCount = (int)Mathf.Ceil(player.healthComponent.MaxHealth / 2.0f);

		GD.Print($"Heart count:{heartCount}");

		player.healthComponent.MaxHealthChanged += OnUpdateMaxHealth;
		player.healthComponent.HealthChanged += OnUpdateHealth;
	}

	private void OnUpdateMaxHealth(int newMaxHealth)
	{

	}

	private void OnUpdateHealth(int newHealth)
	{

	}
}
