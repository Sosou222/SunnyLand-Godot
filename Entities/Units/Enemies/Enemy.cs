using Godot;
using System;

public partial class Enemy : Unit
{
    [Export]
    private RayCast2D toPlayerRaycast;

    private bool shouldForget = false;

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

    public bool IsPlayerInSight()
    {
        if (player == null)
            return false;
        toPlayerRaycast.TargetPosition = toPlayerRaycast.ToLocal(player.GlobalPosition);
        toPlayerRaycast.ForceRaycastUpdate();
        return !toPlayerRaycast.IsColliding();
    }

    public Vector2 GetDirToPlayer()
    {
        if (player == null)
            return Vector2.Zero;
        return (player.GlobalPosition - GlobalPosition).Normalized();
    }
}
