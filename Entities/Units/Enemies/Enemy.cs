using Godot;
using System;

public partial class Enemy : Unit
{
    [Export]
    private RayCast2D toPlayerRaycast;

    private Player player = null;
    private void OnBodyEntered(Node2D node)
    {
        if (node is Player p)
        {
            GD.Print("Player entered");
            player = p;
        }
    }

    private void OnBodyExit(Node2D node)
    {
        if (node is Player p)
        {
            GD.Print("Player Exited");
            player = null;
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (player == null)
            return;

        GD.Print(IsPlayerInSight());
    }

    public bool IsPlayerInSight()
    {
        toPlayerRaycast.TargetPosition = player.GlobalPosition;
        return !toPlayerRaycast.IsColliding();
    }
}
