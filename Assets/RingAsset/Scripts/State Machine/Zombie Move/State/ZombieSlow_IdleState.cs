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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (zombieSlow._botController._isFindPlayer)
        {
            Debug.LogError("Player");
            Vector3 direction = PlayerController.Instance.transform.position -
                                zombieSlow._botController._transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            float rotationSpeed = 0.1f;
            Quaternion newRotation =
                Quaternion.Lerp(zombieSlow._botController._transform.rotation, rotation, rotationSpeed);
            zombieSlow._botController._transform.rotation = newRotation;
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
