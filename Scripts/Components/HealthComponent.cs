using Godot;
using System;

[GlobalClass]
public partial class HealthComponent : Node
{
    [Export(PropertyHint.Range, "0,20,1")]
    private int health = 6;
    [Export(PropertyHint.Range, "0,20,1")]
    private int maxHealth = 6;
    [Export(PropertyHint.Range, "0,100,1")]
    private int maxHealthHardCap = 20;
    [Export]
    private bool SetHealthToMaxAtBegin = true;
    public int Health
    {
        get { return health; }
        private set { health = value; EmitSignal(SignalName.HealthChanged, health); }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
        private set { maxHealth = value; EmitSignal(SignalName.MaxHealthChanged, maxHealth); }
    }


    [Signal]
    public delegate void MaxHealthChangedEventHandler(int newMaxHealth);

    [Signal]
    public delegate void HealthChangedEventHandler(int newHealth);

    [Signal]
    public delegate void TakenDamageEventHandler(int newHealth);

    [Signal]
    public delegate void DeathEventHandler();

    public override void _Ready()
    {
        if (SetHealthToMaxAtBegin)
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

    public void AddMaxHealth(int value = 2)
    {
        MaxHealth = Math.Min(maxHealth + value, maxHealthHardCap);
        Health = MaxHealth;
    }

    public void SetMaxHealth(int value)
    {
        MaxHealth = value;
        Health = MaxHealth;
    }

}
