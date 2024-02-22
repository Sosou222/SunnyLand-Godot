using Godot;
using System;

using Godot.Collections;

public partial class HealthBarUI : Control
{
	[Export]
	Array<TextureRect> heartArray = new Array<TextureRect>();

	[Export]
	Player player;

	private int heartCount = 0;

	public override void _Ready()
	{
		heartCount = (int)Mathf.Ceil(player.healthComponent.MaxHealth / 2.0f);
		ShowHearts(heartCount);
		int hp = player.healthComponent.Health;
		UpdateHearts(hp);


		player.healthComponent.MaxHealthChanged += OnUpdateMaxHealth;
		player.healthComponent.HealthChanged += OnUpdateHealth;
	}

	private void OnUpdateMaxHealth(int newMaxHealth)
	{
		heartCount = (int)Mathf.Ceil(player.healthComponent.MaxHealth / 2.0f);
		ShowHearts(heartCount);
	}

	private void OnUpdateHealth(int newHealth)
	{
		UpdateHearts(newHealth);
	}


	private void ShowHearts(int count)
	{
		for (int i = 0; i < heartArray.Count; i++)
		{
			heartArray[i].Visible = i < count;
		}
	}

	private void UpdateHearts(int health)
	{
		for (int i = 0; i < heartArray.Count; i++)
		{
			if (heartArray[i].Texture is AtlasTexture atlas)
			{
				Rect2 region = atlas.Region;

				if (health - 2 >= 0)
				{
					health -= 2;
					region = new Rect2(new Vector2(0, 0), region.Size);

				}
				else if (health == 1)
				{
					health -= 1;
					region = new Rect2(new Vector2(32, 0), region.Size);
				}
				else
				{
					region = new Rect2(new Vector2(64, 0), region.Size);
				}
				atlas.Region = region;
			}
		}

	}
}
