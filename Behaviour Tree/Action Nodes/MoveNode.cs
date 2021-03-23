using UnityEngine;

public class MoveNode : PhysicsNode
{
    Rigidbody entity;
    Transform target;
    int speed;
    float stopRange;

    GameObject targetHolder;

    public MoveNode(Rigidbody entity, Transform target, int speed, float stopRange) : base()
    {
        this.entity = entity;
        this.target = target;
        this.speed = speed;
        this.stopRange = stopRange;
    }

    public MoveNode(Rigidbody entity, Vector3 pTarget, int speed, float stopRange) : base()
    {
        this.entity = entity;
        this.speed = speed;
        this.stopRange = stopRange;

        if(targetHolder == null)
        {
            targetHolder = new GameObject("Target Holder");
            // targetHolder.transform.parent = entity.transform;
        }
        
        target = targetHolder.transform;
        target.position = pTarget;
    }

    public override NodeState Evaluate()
    {
        if(Vector3.Distance(entity.position, target.position) <= stopRange)
            return SetState(NodeState.SUCCESS);

        Vector3 dir = target.position - entity.position;
        Quaternion rot = Quaternion.LookRotation(dir);

        entity.MoveRotation(Quaternion.Slerp(entity.rotation, rot, Time.fixedDeltaTime * speed * 1.5f));
        entity.MovePosition(entity.position + entity.transform.forward * Time.fixedDeltaTime * speed);

        return SetState(NodeState.RUNNING);
    }

    public void SetTarget(Transform target) { this.target = target; }
    public void SetTarget(Vector3 target) { this.target.position = target; }
}