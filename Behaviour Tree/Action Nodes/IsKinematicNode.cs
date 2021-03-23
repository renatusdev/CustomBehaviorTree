using UnityEngine;

public class IsKinematicNode : Node
{
    Rigidbody rb;
    bool kState;

    public IsKinematicNode(Rigidbody rigidbody, bool kState)
    {
        this.rb = rigidbody;
        this.kState = kState;
    }

    public override NodeState Evaluate()
    {
        rb.isKinematic = kState;
        return SetState(NodeState.SUCCESS); 
    }
}