using Godot;
using System;

[GlobalClass]
public abstract partial class PlayerState : BaseState
{
    protected Player owner;

    public enum StateNames
    {
        Idle,
        Walk,
        Jump,
        Crouch
    }

    public override bool Init<T>(T owner)
    {
        if (owner as Player == null)
        {
            return false;
        }
        this.owner = owner as Player;
        return true;
    }

}
