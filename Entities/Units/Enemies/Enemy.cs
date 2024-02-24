using Godot;
using System;

public partial class Enemy : Unit
{
    [Export]
    private RayCast2D toPlayerRaycast;

    private bool shouldForget = false;
    private bool tryToHurtPlayer = false;

    //from 0 to 1.0 but probably somenthing more like 0.99 ~ 0.98
    private float stompThreshhold = 0.5f;

    private Player player = null;

    public override void _Process(double delta)
    {
        if (tryToHurtPlayer && player != null)
        {
            player.Hurt();
        }
    }

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

    private void OnHitboxEnter(Node2D node)
    {
        if (node is Player p)
        {
            var posHit = GetNode<Area2D>("HitBox").GlobalPosition;
            var posPlayer = player.GlobalPosition;
            var normal = (posHit - posPlayer).Normalized();
            if (normal.Y > stompThreshhold)
            {
                GetNode<Area2D>("HitBox").SetDeferred("monitoring", false);
                p.Jump();
                stateMachine.ChangeState("Death");
            }

        }
    }

    private void OnHurtBoxEnter(Node2D node)
    {
        if (node is Player)
        {
            GD.Print("Hurt box enter");
            if (stateMachine.CurrentStateName != "Death")
                tryToHurtPlayer = true;
        }
    }

    private void OnHurtBoxExit(Node2D node)
    {
        if (node is Player)
        {
            GD.Print("Hurt box exit");
            tryToHurtPlayer = false;
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
