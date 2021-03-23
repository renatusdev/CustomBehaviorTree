using UnityEngine;

public class FlagNode : Node
{
    NodeState startState, flagState;

    public FlagNode(NodeState startState, NodeState flagState)
    {
        this.startState = startState;
        this.flagState = flagState;
        SetState(startState);
    }

    public override NodeState Evaluate()
    {
        return GetState();
    }

    public void Flag(){ SetState(flagState); }
}