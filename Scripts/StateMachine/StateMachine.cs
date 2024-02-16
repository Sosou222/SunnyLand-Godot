using Godot;
using System;

using Godot.Collections;
using System.Linq;

[GlobalClass]
public partial class StateMachine : Node
{
    protected Dictionary<string, BaseState<Node>> states = new Dictionary<string, BaseState<Node>>();
    public BaseState<Node> CurrentState { get; private set; }

    public string CurrentStateName { get; private set; }

    [Export]
    private string initialState;

    public void Init<T>(T owner) where T : Node
    {
        
        GD.Print("Initalizaing states");
        GD.Print($"Dictonary Value Type:{states.Values.GetType()}");
        foreach(var child in GetChildren().OfType<BaseState<T>>())
        {
            GD.Print($"Trying to add child:{child.Name} and is type:{child.GetType()} is subclass:{child.GetType().IsSubclassOf(states.Values.GetType())}");
            child.Init(owner);
            Add(child.Name, child);
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

    public void Add<T>(string name, BaseState<T> state) where T : Node
    {
        var tmp = state as BaseState<Node>;
        if (tmp == null)
            GD.Print("Is Null");
        states[name] = state as BaseState<Node>;
        state.stateMachine = this;
        GD.Print($"AddedCurrentCount:{states.Count}");
    }

    public void ChangeState(string name)
    {
        if (CurrentStateName == name || !states.ContainsKey(name))
            return;

        CurrentState.Exit();
        CurrentState = states[name];
        CurrentStateName = name;
        CurrentState.Enter();
    }

    public void Update(double delta)
    {
        //CurrentState.Update(delta);
    }

    public void PhysicsUpdate(double delta)
    {
        //CurrentState.PhysicsUpdate(delta);
    }

    public void ProcessInput(InputEvent input)
    {
        //CurrentState.ProcessInput(input);
    }
}