using Godot;
using System;

public partial class HealthComponent : Node
{
    [Export]
    private int Health = 3;
    [Export]
    private int MaxHealth = 3;

    [Signal]
    public delegate void TakenDamageEventHandler(int newHealth);

    [Signal]
    public delegate void DeathEventHandler();

    public override void _Ready()
    {
        Health = MaxHealth;
    }


    public void TakeDamage(int damage = 1)
    {
        Health = Mathf.Max(Health - damage, 0);
        EmitSignal(SignalName.TakenDamage, Health);
        if (Health == 0)
        {
            EmitSignal(SignalName.Death);
        }
    }

    public void Heal(int heal = 1)
    {
        Health = Mathf.Min(Health + heal, MaxHealth);
    }


}
