using Godot;
using System;

[GlobalClass]
public partial class PlayerHurtComponent : Area2D
{
    [Export]
    private StateMachine stateMachine;
    private Player player;

    public override void _Process(double delta)
    {
        if (player != null)
        {
            player.Hurt(GetDirXToPlayer() > 0.0f ? false : true);
        }
    }

    private void OnHurtBoxEnter(Node2D node)
    {
        if (node is Player p)
        {
            GD.Print("Hurt box enter");
            if (stateMachine.CurrentStateName != "Death")
            {
                player = p;
            }
        }
    }

    private void OnHurtBoxExit(Node2D node)
    {
        if (node is Player p)
        {
            GD.Print("Hurt box exit");
            player = null;
        }
    }

    private float GetDirXToPlayer()
    {
        if (player == null)
            return 0.0f;
        return (player.GlobalPosition - GlobalPosition).Normalized().X;
    }
}
