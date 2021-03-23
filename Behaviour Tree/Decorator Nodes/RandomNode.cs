using UnityEngine;

public class RandomNode : Node
{
    Node n;
    float probability;

    public RandomNode(Node n, float probability)
    {
        this.n = n;
        this.probability = probability;
    }

    public override NodeState Evaluate()
    {
        if(probability >= Random.value)
            return n.Evaluate();
        else
            return NodeState.FAILURE;
    }
}