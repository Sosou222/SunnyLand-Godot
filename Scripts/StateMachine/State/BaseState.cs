using Godot;
using System;

public partial class BaseState : Node 
{
    public StateMachine stateMachine;

    public virtual bool Init<T>(T owner) where T : Node { return true; }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update(double delta) { }
    public virtual void PhysicsUpdate(double delta) { }
    public virtual void ProcessInput(InputEvent input) { }

}

