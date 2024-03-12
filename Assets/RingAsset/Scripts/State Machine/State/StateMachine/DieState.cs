
using UnityEngine;

public class DieState : State
{
    public DieState(BotManager botManager, FiniteStateMachine stateMachine) : base(botManager, stateMachine)
    {
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