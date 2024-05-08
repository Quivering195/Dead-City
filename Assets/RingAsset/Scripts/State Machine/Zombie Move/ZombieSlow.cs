using UnityEngine;

public class ZombieSlow : BotManager
{
    public ZombieSlow_IdleState idleState { get; private set; }
    public ZombieSlow_MoveState moveState { get; private set; }
    public ZombieSlow_AttackState attackState { get; private set; }
    [SerializeField] private D_IdleState _idleState;
    [SerializeField] private D_MoveState _moveState;
    [SerializeField] public D_AttackState _attackState;

    public override void Start()
    {
        base.Start();
        moveState = new ZombieSlow_MoveState(this, stateMachine, "Move", _moveState, this);
        idleState = new ZombieSlow_IdleState(this, stateMachine, "Idle", _idleState, this);
        attackState = new ZombieSlow_AttackState(this, stateMachine, "Attack", _attackState, this);
        stateMachine.Initialize(idleState);
    }

    public override void Update()
    {
        base.Update();
        CheckPositionPlayer();
    }

    private void CheckPositionPlayer()
    {
        Collider[] colliders =
            Physics.OverlapSphere(transform.position, _botController._radius, _botController._layerMask);

        bool foundPlayer = false; // Biến để kiểm tra xem có người chơi nào được tìm thấy không

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(Settings.Tag_Player))
            {
                foundPlayer = true;
                break; // Thoát khỏi vòng lặp ngay khi tìm thấy người chơi
            }
        }

        if (foundPlayer && !_botController._isFindPlayer)
        {
            _botController._isFindPlayer = true;
            Debug.Log("Player detected within the sphere!" + _botController._isFindPlayer);
        }
        else if (!foundPlayer)
        {
            _botController._isFindPlayer = false;
        }
    }

//
    public LayerMask playerLayer; // Layer của player
    public float radius = 2f; // Bán kính của vùng kiểm tra

    public void CheckForPlayersInRange()
    {
        // Tìm tất cả các collider trong vùng phạm vi
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, playerLayer);

        // Kiểm tra từng collider nếu nó là player
        foreach (Collider col in colliders)
        {
            // Gửi thông điệp đến PlayerController nếu collider có tag là player
            if (col.CompareTag("Player"))
            {
                PlayerController.Instance.GetAttack(-(int)_attackState.damage);
            }
        }
    }

//
    private void OnDrawGizmos()
    {
        // Vẽ hình cầu trong Scene
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _botController._radius);
    }
}
