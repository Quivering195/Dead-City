using UnityEngine;

public class State
{
    protected FiniteStateMachine stateMachine;
    protected BotManager botController;
    protected float startTime;
    protected string animName;

    public State(BotManager botController, FiniteStateMachine stateMachine, string animName)
    {
        this.botController = botController;
        this.stateMachine = stateMachine;
        this.animName = animName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        botController._botController._animator.SetBool(animName, true);
    }

    public virtual void Exit()
    {
        botController._botController._animator.SetBool(animName, false);
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }
}