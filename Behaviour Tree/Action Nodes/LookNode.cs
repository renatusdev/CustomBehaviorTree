using UnityEngine;

public class LookNode : Node
{
    public readonly static int angleSpread = 40;

    Rigidbody entity;
    Transform target;
    float accuracy;
    int speed, minAngleForLookComplete;

    public LookNode(Rigidbody entity, Transform target, float accuracy, int speed, int minAngleForLookComplete)
    {
        this.entity = entity;
        this.target = target;
        this.accuracy = accuracy;
        this.speed = speed;
        this.minAngleForLookComplete = minAngleForLookComplete;
    }
    
    public override NodeState Evaluate()
    {
        Quaternion newRot = Quaternion.LookRotation((target.position - entity.position));
        Quaternion angle = Quaternion.Euler(0,angleSpread - (angleSpread*accuracy),0);

        newRot *= angle;

        if(Quaternion.Angle(entity.rotation, newRot) <=  minAngleForLookComplete)
            return SetState(NodeState.SUCCESS);
        
        
        entity.MoveRotation(Quaternion.Slerp(entity.rotation, 
        newRot, Time.deltaTime * speed));

        return SetState(NodeState.RUNNING);
    }
}