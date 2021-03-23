using UnityEngine;

public class ResetNode : Node
{
    Node[] nodes;

    public ResetNode(params Node[] nodes)
    {
        this.nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        foreach(Node n in nodes)
            n.Reset();
        
        return SetState(NodeState.SUCCESS);
    }
}