using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// If a node returns SUCCESS, then the sequence continues to the next one.
// If a node returns RUNNING or FAILURE, then the sequence stops its loop on the running node. (and reevaluate it the next frame).
public class SequenceNode : CompositeNode
{
    public SequenceNode( List<Node> nodes)
    {
        this.nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        i = 0;
        
        while(i < nodes.Count)
        {
            if(nodes[i].isPhysicsBased & !BehaviorTreeManager.IsFixedTime())
                return SetState(NodeState.RUNNING);

            NodeState status = nodes[i].Evaluate();

            if(status.Equals(NodeState.SUCCESS))
            {
                // The current node was a success, lets continue to the next.
                i++;
            }
            else// if(status.Equals(NodeState.FAILURE) || status.Equals(NodeState.RUNNING))
            {
                // Keep current index and stop the loop with the current status.
                return SetState(status);
            }
        }
        
        // If this code path is reached, then all nodes where a success.
        return SetState(NodeState.SUCCESS);
    }
}