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

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (Input.IsActionJustReleased("PlayerRight"))
        {
            healthComponent.TakeDamage();
        }
        if (Input.IsActionJustReleased("PlayerLeft"))
        {
            healthComponent.Heal();
        }
        if (Input.IsActionJustReleased("PlayerJump"))
        {
            healthComponent.AddMaxHealth();
        }
    }
}
