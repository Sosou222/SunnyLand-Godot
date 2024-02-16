using Godot;
using System;

[GlobalClass]
public partial class IdleUnitState : BaseState<CharacterBody2D>
{
    public override void Enter()
    {
        GD.Print($"My owner is {owner.Name}");
    }
}
