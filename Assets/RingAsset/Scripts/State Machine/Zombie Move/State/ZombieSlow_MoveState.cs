using UnityEngine;

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
        zombieSlow._botController._animator.SetBool("Move", true);
        zombieSlow._botController._animator.SetBool("Attack", false);
        zombieSlow._botController._animator.SetBool("Idle", false);
    }

    public override void Exit()
    {
        base.Exit();
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

        // Di chuyển zombie tới vị trí của player trên mặt phẳng x-z
        float moveSpeed = 1f; // Tốc độ di chuyển
        Vector3 movement = direction.normalized * moveSpeed * Time.deltaTime;
        zombieSlow._botController._rigidbody.MovePosition(zombieSlow._botController._rigidbody.position + movement);
        if (Vector3.Distance(zombieSlow.transform.position, PlayerController.Instance.transform.position) < 2)
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
