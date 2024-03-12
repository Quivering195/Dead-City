using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSlow_IdleState : IdleState
{
    protected ZombieSlow zombieSlow;

    public ZombieSlow_IdleState(BotManager botManager, FiniteStateMachine stateMachine, D_IdleState stateData, ZombieSlow zombieSlow) : base(botManager, stateMachine, stateData)
    {
        this.zombieSlow = zombieSlow;
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