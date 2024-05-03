using UnityEngine;

public class ZombieSlow_IdleState : IdleState
{
    protected ZombieSlow zombieSlow;

    public ZombieSlow_IdleState(BotManager botManager, FiniteStateMachine stateMachine, string animName,
        D_IdleState stateData,
        ZombieSlow zombieSlow) : base(botManager, stateMachine, animName, stateData)
    {
        this.zombieSlow = zombieSlow;
    }

    public override void Enter()
    {
        base.Enter();
        zombieSlow._botController._animator.SetBool("Idle", true);
        zombieSlow._botController._animator.SetBool("Attack", false);
        zombieSlow._botController._animator.SetBool("Move", false);
    }

    public override void Exit()
    {
        base.Exit();
        //zombieSlow._botController._animator.SetBool("Idle", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (zombieSlow._botController._isFindPlayer)
        {
            Debug.LogError("Player");
            if (Vector3.Distance(zombieSlow.transform.position, PlayerController.Instance.transform.position) < 2)
            {
                Debug.Log("Attack");
                stateMachine.ChangeState(zombieSlow.attackState);
            }
            else
            {
                stateMachine.ChangeState(zombieSlow.moveState);
            }
        }
        else
        {
            Debug.LogError("not Player");
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
