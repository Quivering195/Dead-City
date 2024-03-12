using UnityEngine;

public class IdleState : State
{
    protected D_IdleState stateData;
    public IdleState(BotManager botManager, FiniteStateMachine stateMachine, D_IdleState stateData) : base(botManager, stateMachine)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}