using UnityEngine;

public class ZombieSlow : BotManager
{
    public ZombieSlow_IdleState idleState { get; private set; }
    public ZombieSlow_MoveState moveState { get; private set; }
    [SerializeField] private D_IdleState _idleState;
    [SerializeField] private D_MoveState _moveState;

    public override void Start()
    {
        base.Start();
        moveState = new ZombieSlow_MoveState(this, stateMachine, _moveState, this);
        idleState = new ZombieSlow_IdleState(this, stateMachine, _idleState, this);
        stateMachine.Initialize(idleState);
    }
}