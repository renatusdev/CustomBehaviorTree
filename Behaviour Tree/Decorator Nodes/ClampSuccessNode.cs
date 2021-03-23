using UnityEngine;

public class ClampSuccessNode : Node
{
    Node n;
    bool clamped;

    public ClampSuccessNode(Node n)
    {
        this.n = n;
        this.clamped = false;
    }

    public override NodeState Evaluate()
    {
        if(clamped)
            return NodeState.SUCCESS;
            
        if(n.GetState().Equals(NodeState.SUCCESS))
        {
            clamped = true;
            return SetState(NodeState.SUCCESS);
        }

        return n.Evaluate();
    }

    public override void Reset()
    {
        base.Reset();
        clamped = false;
    }
}