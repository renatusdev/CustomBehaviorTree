using UnityEngine;

public class EvadeNode : PhysicsNode
{
    Rigidbody entity;
    int speed;
    float radiusSize;
    float dodgeDistance;

    public Vector3 dodgeDirection;

    public EvadeNode(Rigidbody entity, int speed, float radiusSize, float dodgeDistance) : base()
    {
        this.entity = entity;
        this.speed = speed;
        this.radiusSize = radiusSize;
        this.dodgeDistance=  dodgeDistance;
        this.dodgeDirection = Vector3.zero;
    }

    public override NodeState Evaluate()
    {
        if(dodgeDirection.Equals(Vector3.zero))
        {            
            for(int i = 0; i < EvadeHelper.rayDirections.Length; i++)
            {
                Vector3 dir = entity.transform.TransformDirection (EvadeHelper.rayDirections[i]);
                Ray ray = new Ray (entity.position, dir);

                Debug.DrawRay(entity.position, dir * dodgeDistance, Color.blue);

                if(!Physics.SphereCast(ray, radiusSize, dodgeDistance, LayerMask.GetMask("Obstacle")))
                {
                    dodgeDirection = dir;
                    break;
                }
            }
        }

        if(!dodgeDirection.Equals(Vector3.zero))
        {
            Debug.DrawRay(entity.position, dodgeDirection * dodgeDistance, Color.red);
            // Debug.Break();
        }

        Quaternion rot = Quaternion.LookRotation(dodgeDirection);
        entity.MoveRotation(Quaternion.Slerp(entity.rotation, rot, Time.fixedDeltaTime * speed * 1.5f));
        entity.MovePosition(entity.position + entity.transform.forward * Time.fixedDeltaTime * speed * 0.75f);

        return SetState(NodeState.SUCCESS);
    }
}

public static class EvadeHelper
{
    static int numViewDirections = 1000;
    public static readonly Vector3[] rayDirections = new Vector3[numViewDirections];

    static EvadeHelper() 
    {
        float goldenRatio = (1 + Mathf.Sqrt (5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;

        for (int i = 0; i < numViewDirections; i++) {
            float t = (float) i / numViewDirections;
            float inclination = Mathf.Acos (1 - 2 * t);
            float azimuth = angleIncrement * i;

            float x = Mathf.Sin (inclination) * Mathf.Cos (azimuth);
            float y = Mathf.Sin (inclination) * Mathf.Sin (azimuth);
            float z = Mathf.Cos (inclination);
            rayDirections[i] = new Vector3 (x, y, z);
        }
    }
}