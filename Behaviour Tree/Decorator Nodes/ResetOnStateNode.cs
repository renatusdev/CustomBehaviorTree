using UnityEngine;

public class ResetOnStateNode : Node
{
    Node n;
    NodeState tS;

    public ResetOnStateNode(Node node, NodeState triggerState)
    {
        this.n = node;
        this.tS = triggerState;
    }

    public override NodeState Evaluate()
    {
        if(n.GetState().Equals(tS))
            n.Reset();
        return SetState(n.Evaluate());
    }
}