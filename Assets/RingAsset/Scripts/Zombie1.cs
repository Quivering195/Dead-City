using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie1 : BotManager
{
    public AttackState attackState { get; private set; }
    public MoveState moveState { get; private set; }
    public IdleState idleState { get; private set; }
    public D_IdleState D_IdleState { get; private set; }

    private void Awake()
    {
        idleState = new IdleState(this, stateMachine, D_IdleState);
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }
}
