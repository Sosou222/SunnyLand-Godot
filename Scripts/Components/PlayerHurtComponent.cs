using Godot;
using System;

public partial class PlayerHurtComponent : Node
{
    [Export]
    private StateMachine stateMachine;
    private Player player;

    public override void _Process(double delta)
    {
        if (player != null)
        {
            player.Hurt();
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
}
