using Godot;
using System;

public abstract partial class EnemyState : BaseState
{
    protected Enemy owner;

    protected float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override bool Init<T>(T owner)
    {
        if (owner as Enemy == null)
        {
            return false;
        }
        this.owner = owner as Enemy;
        return true;
    }

    protected Vector2 CalculateGravity(Vector2 velocity, double delta)
    {
        if (!owner.IsOnFloor())
            velocity.Y += gravity * (float)delta;
        return velocity;
    }
}
