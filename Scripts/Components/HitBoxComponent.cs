using Godot;
using System;

public partial class HitBoxComponent : Area2D
{
	[Export(PropertyHint.Range, "0.0,1.0,0.01")]
	private float stompThreshhold = 0.5f;

	[Export]
	StateMachine stateMachine;

	private void OnHitboxEnter(Node2D node)
	{
		if (node is Player p)
		{
			var posHit = GlobalPosition;
			var posPlayer = p.GlobalPosition;
			var normal = (posHit - posPlayer).Normalized();
			if (normal.Y > stompThreshhold)
			{
				SetDeferred("monitoring", false);
				p.Jump();
				stateMachine.ChangeState("Death");
			}

		}
	}
}
