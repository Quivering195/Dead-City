using System.Diagnostics;
using System.IO;
using Ring;
using UnityEditor;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    [HeaderTextColor(0.2f, .7f, .8f, headerText = "Player Component")]
    public BotController _botController;

    public FiniteStateMachine stateMachine;

    public virtual void Start()
    {
        Intil();
        StateMachineBot();
    }

    private void StateMachineBot()
    {
        stateMachine = new FiniteStateMachine();
    }

    private void Intil()
    {
    }

    public void ActiveRagdoll()
    {
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetBackDamage(float direction)
    {
        _botController._rigidbody.AddForce(_botController._directionBackDamage * 25, ForceMode.Impulse);
    }


}
