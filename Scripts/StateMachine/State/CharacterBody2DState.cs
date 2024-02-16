using Godot;
using System;

public partial class CharacterBody2DState : BaseState
{
    protected CharacterBody2D owner;

    public override void Init(Node owner)
    {
        if (owner is  CharacterBody2D) 
            owner = (CharacterBody2D)owner;
    }
}
