using Godot;
using System;

using Godot.Collections;
using System.Linq;

[GlobalClass]
public partial class StateMachine : Node
{
    protected Dictionary<string, BaseState> states = new Dictionary<string, BaseState>();
    public BaseState CurrentState { get; private set; }

    public string CurrentStateName { get; private set; }

    [Export]
    private string initialState;

    public void Init<T>(T owner) where T : Node
    {

        foreach (var child in GetChildren().OfType<BaseState>())
        {
            if (child.Init(owner))
            {
                Add(child.Name, child);
            }
        }

        InitalState(initialState);
    }

    private void InitalState(string name)
    {
        if (!states.ContainsKey(name))
        {
            GD.PrintErr($"Inital scene not defined beacuse given name:{initialState} does not exists in dictiorany");
            return;
        }
        CurrentState = states[name];
        CurrentStateName = name;
        CurrentState.Enter();
    }

    public void Add(string name, BaseState state)
    {
        states[name] = state;
        state.stateMachine = this;
    }

    public void ChangeState(string name)
    {
        if (CurrentStateName == name || !states.ContainsKey(name))
        {
            return;
        }


        CurrentState.Exit();
        CurrentState = states[name];
        CurrentStateName = name;
        CurrentState.Enter();
    }

    public void Update(double delta)
    {
        CurrentState.Update(delta);
    }

    public void PhysicsUpdate(double delta)
    {
        CurrentState.PhysicsUpdate(delta);
    }

    public void ProcessInput(InputEvent input)
    {
        CurrentState.ProcessInput(input);
    }
}