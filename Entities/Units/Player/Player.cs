using Godot;
using System;

public partial class Player : Unit
{
    private int cherryCount = 0;
    private int gemCount = 0;

    [Export]
    public HealthComponent healthComponent { get; private set; }

    [Signal]
    public delegate void CherryCountChangeEventHandler(int newValue);
    [Signal]
    public delegate void GemCountChangeEventHandler(int newValue);

    public void AddCollectible(string name, int amount = 1)
    {
        if (name == "Cherry")
        {
            cherryCount += amount;
            EmitSignal(SignalName.CherryCountChange, cherryCount);
            GD.Print($"Cherry count:{cherryCount}");
        }
        if (name == "Gem")
        {
            gemCount += amount;
            EmitSignal(SignalName.GemCountChange, gemCount);
            GD.Print($"Gem count:{gemCount}");
        }
    }

    public void Jump()
    {
        if (stateMachine.CurrentStateName == PlayerState.StateNames.Jump.ToString())
            stateMachine.ChangeState(PlayerState.StateNames.Fall.ToString());
        stateMachine.ChangeState(PlayerState.StateNames.Jump.ToString());
    }
}
