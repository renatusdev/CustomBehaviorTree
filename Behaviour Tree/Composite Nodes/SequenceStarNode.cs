// using UnityEngine;
// using System.Collections.Generic;

// // If a node returns SUCCESS, then the sequence continues to the next one.
// // If a node returns RUNNING or FAILURE, then the sequence stops its loop on the running node. (and reevaluate it the next frame).
// public class SequenceStarNode : CompositeNode
// {
//     public SequenceStarNode(List<Node> nodes) { this.nodes = nodes; }

//     public override NodeState Evaluate()
//     {
//         while(i < nodes.Count)
//         {
//             NodeState status = nodes[i].Evaluate();

//             if(status.Equals(NodeState.SUCCESS))
//             {
//                 // The current state was a success, lets continue to the next.
//                 i++;
//             }
//             else if(status.Equals(NodeState.FAILURE) || status.Equals(NodeState.RUNNING))
//             {
//                 // Keep current index and stop the loop with the current status.
//                 return SetState(status);
//             }
//         }

//         // If this code path is reached, then all nodes where a success.
//         return SetState(NodeState.SUCCESS);
//     }
// }