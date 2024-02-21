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

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (player == null)
            return;

        IsPlayerInSight();
    }

    public bool IsPlayerInSight()
    {
        toPlayerRaycast.TargetPosition = toPlayerRaycast.ToLocal(player.GlobalPosition);
        toPlayerRaycast.ForceRaycastUpdate();
        return !toPlayerRaycast.IsColliding();
    }
}
