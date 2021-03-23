using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterNode : Node
{
    protected Node node;

    public InverterNode (Node node) { this.node = node; }

    public override NodeState Evaluate()
    {
        switch (node.Evaluate())
        {
            case NodeState.SUCCESS: return SetState(NodeState.FAILURE);
            case NodeState.RUNNING: return SetState(NodeState.RUNNING);
            case NodeState.FAILURE: return SetState(NodeState.SUCCESS);
            default: throw new System.Exception();
        }
    }
}
