using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// If a node returns SUCCESS, then the sequence continues to the next one.
// If a node returns RUNNING or FAILURE, then the sequence stops its loop on the running node. (and reevaluate it the next frame).
public class SelectorNode : CompositeNode 
{

    public SelectorNode(List<Node> nodes)
    {
        this.nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        i = 0;
        while(i < nodes.Count)
        {
            NodeState status = nodes[i].Evaluate();

            if(status.Equals(NodeState.FAILURE))
            {
                // The current node was a failure, lets continue to the next.
                i++;
            }
            else// if(status.Equals(NodeState.RUNNING) || status.Equals(NodeState.SUCCESS))
            {
                // Keep current index and stop the loop with the current status.
                return SetState(status);
            }
        }

        // If this code path is reached, then all nodes where a failure.
        return SetState(NodeState.FAILURE);
    }
}
