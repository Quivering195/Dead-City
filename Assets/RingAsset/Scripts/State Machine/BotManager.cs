using Ring;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    [HeaderTextColor(0.2f, .7f, .8f, headerText = "Player Component")] public BotManager _botController;
    
    public FiniteStateMachine stateMachine;

    public virtual void Start()
    {
        Intil();
        StateMachineBot();
    }

    private void StateMachineBot()
    {
        //getComponentBot
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
}