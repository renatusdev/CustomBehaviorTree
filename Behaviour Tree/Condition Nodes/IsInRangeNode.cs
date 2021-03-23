using UnityEngine;

public class IsInRangeNode : Node
{
    float range;
    Transform origin;
    Transform target;

    public IsInRangeNode(float range, Transform origin, Transform target)
    {
        this.range = range; this.origin = origin; this.target = target;
    }

    public override NodeState Evaluate()
    {
        return Vector3.Distance(origin.position, target.position)
            <= range ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}