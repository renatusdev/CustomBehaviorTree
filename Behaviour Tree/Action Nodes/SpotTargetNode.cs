using UnityEngine;

public class SpotTargetNode : PhysicsNode
{
    int viewAngle;
    int viewRadius;
    int targetMask;
    int selfMask;
    Transform entity;
    Collider[] hitColliders;

    public SpotTargetNode(Transform entity, int viewAngle, int viewRadius, int targetMask, int selfMask) : base()
    {
        this.entity = entity;
        this.targetMask = targetMask;
        this.selfMask = selfMask;
        this.viewAngle = viewAngle;
        this.viewRadius = viewRadius;
        hitColliders = new Collider[1];
    }

    public override NodeState Evaluate()
    {
        // Cast a sphere to verify if collider with mask is near.   
        if(Physics.OverlapSphereNonAlloc(entity.position, viewRadius, hitColliders, 1<<targetMask) != 0)
        {
            Transform target = hitColliders[0].transform;
            Vector3 forward = XZPlane(entity.forward);
            Vector3 direction = XZPlane(target.position-entity.position).normalized;

            // Calculates if the angle between the entity and new target is within the entities view range.
            if(Vector3.Angle(forward, direction) <= viewAngle)
            {
                // Calculates if it is the case that something is in between the target and the self.
                if(!Physics.Linecast(entity.position, target.position, ~(1<<targetMask | 1<<selfMask)))
                {
                    return SetState(NodeState.SUCCESS);
                }
            }
        }
            
        return SetState(NodeState.FAILURE);
    }

    private static Vector3 XZPlane(Vector3 v) { return new Vector3(v.x, 0, v.z); }
}
