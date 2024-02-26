using Godot;
using System;

public partial class Enemy : Unit
{
    [Export]
    public PlayerDetectionComponent pdc { private set; get; }
}
