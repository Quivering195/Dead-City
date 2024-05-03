using UnityEngine;

public class ZombieSlow_AttackState : AttackState
{
    protected ZombieSlow zombieSlow;

    public ZombieSlow_AttackState(BotManager botManager, FiniteStateMachine stateMachine, string animName,
        D_AttackState dataState, ZombieSlow zombieSlow) : base(botManager, stateMachine, animName, dataState)
    {
        this.zombieSlow = zombieSlow;
    }

    public override void Enter()
    {
        base.Enter();
        zombieSlow._botController._animator.SetBool("Attack", true);
        zombieSlow._botController._animator.SetBool("Idle", false);
        zombieSlow._botController._animator.SetBool("Move", false);
    }

    public override void Exit()
    {
        base.Exit();
        //zombieSlow._botController._animator.SetBool("Attack", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // Tính toán hướng vector giữa zombie và player trên mặt phẳng x-z
        Vector3 playerPos = PlayerController.Instance.transform.position;
        Vector3 zombiePos = zombieSlow._botController._transform.position;
        Vector3 direction = new Vector3(playerPos.x - zombiePos.x, 0f, playerPos.z - zombiePos.z);

        // Tính toán hướng quay của zombie để nhìn về player
        Quaternion rotation = Quaternion.LookRotation(direction);
        float rotationSpeed = 0.1f;
        Quaternion newRotation =
            Quaternion.Lerp(zombieSlow._botController._transform.rotation, rotation, rotationSpeed);
        zombieSlow._botController._transform.rotation = newRotation;
        if (Vector3.Distance(zombieSlow.transform.position, PlayerController.Instance.transform.position) >= 2)
        {
            Debug.Log("Attack");
            stateMachine.ChangeState(zombieSlow.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
