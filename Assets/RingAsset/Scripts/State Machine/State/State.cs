using UnityEngine;

public class State
{
    public FiniteStateMachine stateMachine;
    public BotManager botController;

    public State(BotManager botController, FiniteStateMachine stateMachine)
    {
        this.botController = botController;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }
    public virtual void LogicUpdate()
    {
        
    }
    public virtual void PhysicsUpdate()
    {

    }
}