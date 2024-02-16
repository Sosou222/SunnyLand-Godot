using Godot;
using System;

public partial class AbstractState<N> : BaseState where N : Node
{
    protected N owner;
    public override bool Init<T>(T owner)
    {
        if (owner as N == null)
        {
            GD.PrintErr($"Mismatching type of owner:{owner.Name}. Type T:{typeof(T)} cannot be converted as type:{typeof(N)}");
            return false;
        }
        this.owner = owner as N;
        return true;
    }
}
