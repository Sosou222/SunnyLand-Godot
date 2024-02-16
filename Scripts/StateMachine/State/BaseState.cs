using Godot;
using System;

public partial class BaseState<T> : Node where T : Node
{
    public StateMachine stateMachine;
    protected T owner;

    public void Init(T owner) {
        GD.Print($"Assigning owner as {owner.Name} of type {typeof(T)} and being a subclass of Node:{typeof(T).IsSubclassOf(typeof(Node))}");
        this.owner = owner;

        BaseState<Node> isss = this as BaseState<Node>;
        var node = this as BaseState<Node>;
        GD.Print($"IsNodeNull:{node == null}");
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update(double delta) { }
    public virtual void PhysicsUpdate(double delta) { }
    public virtual void ProcessInput(InputEvent input) { }

}
