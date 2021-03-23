using System.Collections.Generic;
using UnityEngine;

public class ParallelNode : CompositeNode
{
    int threshold;

    public ParallelNode(List<Node> nodes, int threshold)
    {
        this.nodes = nodes;
        this.threshold = threshold;
    }

    public override NodeState Evaluate()
    {
        int i = 0;
        foreach(Node n in nodes)
        {
            switch(n.Evaluate())
            {
                case NodeState.SUCCESS:
                    i++;
                    if(i >= threshold)
                        return SetState(NodeState.SUCCESS);
                    else
                        break;
                case NodeState.RUNNING:
                    break;
                case NodeState.FAILURE:
                    return SetState(NodeState.FAILURE);
            }
        }
        return SetState(NodeState.RUNNING);
    }
}
