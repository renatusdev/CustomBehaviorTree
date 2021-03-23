using UnityEngine;

public enum ForceDirection { forward, backwards, right, left, up, down }
public class AddForceNode : Node
{
    Rigidbody rb;
    ForceMode fM;
    Transform obj;
    Vector3 force;
    ForceDirection forceDirection;
    float magn;

    public AddForceNode(Rigidbody rigidbody, Vector3 force, ForceMode forceMode)
    {
        this.rb= rigidbody;
        this.fM = forceMode;
        this.force = force;
    }

    public AddForceNode(Rigidbody rigidbody, Transform obj, ForceDirection forceDirection, float magnitude, ForceMode forceMode)
    {
        this.rb= rigidbody;
        this.obj = obj;
        this.magn = magnitude;
        this.fM = forceMode;
        this.forceDirection = forceDirection;
    }

    public override NodeState Evaluate()
    {
        if(force.Equals(Vector3.zero))
        {
            switch(forceDirection)
            {
                case ForceDirection.forward:
                    force = obj.forward;
                    break;
                case ForceDirection.backwards:
                    force = -obj.forward;
                    break;
                case ForceDirection.right:
                    force = obj.right;
                    break;
                case ForceDirection.left:
                    force = -obj.right;
                    break;
            }

            rb.AddForce(force * magn, fM);
            force = Vector3.zero;
            return SetState(NodeState.SUCCESS);
        }
        else
        {
            rb.AddForce(force, fM);
            return SetState(NodeState.SUCCESS);
        }
    }
}