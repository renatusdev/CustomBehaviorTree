using UnityEngine;

public class IsBlockedNode : PhysicsNode
{
    Transform entity;
    EvadeNode evade;
    float radiusSize;
    float dodgeDistance;

    public IsBlockedNode (Transform entity, EvadeNode evade, float radiusSize, float dodgeDistance) : base()
    {
        this.entity = entity;
        this.evade = evade;
        this.radiusSize = radiusSize;
        this.dodgeDistance = dodgeDistance;
    }

    public override NodeState Evaluate()
    {            
        if(Physics.SphereCast(new Ray(entity.position, entity.forward), radiusSize, dodgeDistance, LayerMask.GetMask("Obstacle"))
        || Physics.CheckSphere(entity.position, radiusSize, LayerMask.GetMask("Obstacle")))
        {
            return SetState(NodeState.SUCCESS); 
        }
        else
        {
            evade.dodgeDirection = Vector3.zero;
            return SetState(NodeState.FAILURE);
        }
    }
}