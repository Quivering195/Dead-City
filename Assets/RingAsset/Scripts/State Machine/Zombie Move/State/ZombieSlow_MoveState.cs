public class ZombieSlow_MoveState : MoveState
{
    protected ZombieSlow zombieSlow;

    public ZombieSlow_MoveState(BotManager botManager, FiniteStateMachine stateMachine, string animName,
        D_MoveState dataState, ZombieSlow zombieSlow) : base(botManager, stateMachine, animName, dataState)
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
