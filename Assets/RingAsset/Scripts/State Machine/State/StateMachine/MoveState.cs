using UnityEngine;

public class MoveState : State
{
    protected D_MoveState dataState;

    public MoveState(BotManager botManager, FiniteStateMachine stateMachine, string animName, D_MoveState dataState) :
        base(botManager, stateMachine, animName)
    {
        this.dataState = dataState;
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
