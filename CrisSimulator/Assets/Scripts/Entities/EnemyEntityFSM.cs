using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum EnemyEntityState
{
    Iddle,
    Chasing,
    Jumping,
    Attacking,
    Dead
}

public enum EnemyCommand
{
    Chase,
    Jump,
    Attack,
    Stop,
    Die
}

public class EnemyEntityFSM
{
    class StateTransition
    {
        readonly EnemyEntityState CurrentState;
        readonly EnemyCommand Command;
        
        public StateTransition(EnemyEntityState currentState, EnemyCommand command)
        {
            CurrentState = currentState;
            Command = command;
        }
        
        public override int GetHashCode()
        {
            return 17 + 31 * CurrentState.GetHashCode() + 31 * Command.GetHashCode();
        }
        
        public override bool Equals(object obj)
        {
            StateTransition other = obj as StateTransition;
            return other != null && this.CurrentState == other.CurrentState && this.Command == other.Command;
        }
    }
        
    Dictionary<StateTransition, EnemyEntityState> transitions;

    public EnemyEntityState CurrentState { get; private set; }
    
    public EnemyEntityFSM()
    {
        CurrentState = EnemyEntityState.Iddle;
        transitions = new Dictionary<StateTransition, EnemyEntityState>
        {
            { new StateTransition(EnemyEntityState.Iddle, EnemyCommand.Chase), EnemyEntityState.Chasing },
            { new StateTransition(EnemyEntityState.Iddle, EnemyCommand.Jump), EnemyEntityState.Jumping },
            { new StateTransition(EnemyEntityState.Iddle, EnemyCommand.Attack), EnemyEntityState.Attacking },
            { new StateTransition(EnemyEntityState.Iddle, EnemyCommand.Die), EnemyEntityState.Dead },
            { new StateTransition(EnemyEntityState.Chasing, EnemyCommand.Attack), EnemyEntityState.Attacking },
            { new StateTransition(EnemyEntityState.Chasing, EnemyCommand.Jump), EnemyEntityState.Jumping },
            { new StateTransition(EnemyEntityState.Chasing, EnemyCommand.Stop), EnemyEntityState.Iddle },
            { new StateTransition(EnemyEntityState.Chasing, EnemyCommand.Die), EnemyEntityState.Dead },
            { new StateTransition(EnemyEntityState.Jumping, EnemyCommand.Stop), EnemyEntityState.Iddle },
            { new StateTransition(EnemyEntityState.Jumping, EnemyCommand.Die), EnemyEntityState.Dead },
            { new StateTransition(EnemyEntityState.Attacking, EnemyCommand.Stop), EnemyEntityState.Iddle },
            { new StateTransition(EnemyEntityState.Attacking, EnemyCommand.Die), EnemyEntityState.Dead },
        };
    }
    
    public EnemyEntityState GetNext(EnemyCommand command)
    {
        StateTransition transition = new StateTransition(CurrentState, command);
        EnemyEntityState nextState;
        if (!transitions.TryGetValue(transition, out nextState))
            throw new Exception("Invalid transition: " + CurrentState + " -> " + command);
        return nextState;
    }
    
    public EnemyEntityState MoveNext(EnemyCommand command)
    {
        CurrentState = GetNext(command);
        return CurrentState;
    }
}
